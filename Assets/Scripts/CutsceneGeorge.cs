using UnityEngine;
using System.Collections;

// All functions and elements involved in George's Cutscene.
public class CutsceneGeorge : MonoBehaviour {

	public GameObject textPrefab; // Prefab of TextMesh.
	public AudioClip noise;
	public AudioClip flicker;
	public AudioClip[] creepyClips;
	public string[] messages;
	public GameObject spawn;

	public Light[] halluroomLights;

	private GameObject player;

	// Handles Text Flashing
	private GameObject screenText; // text that will flash on the screen
	private bool flash;	// Flash the text in Update?
	private float timeAllot; // allotted time for flashing text

	// Light Flashing
	private float disableFlashing = 1f;

	// Handles 3D Audio Playing
	public AudioClip whisperClip; // Actually the the journal Clip to be played
	private GameObject whisper;   // The game object to be moved around the GeorgeHalluroom

	private Vector3 initialPosition; 
	private bool is3DAudioPlaying;
	private GameObject mc;

	// private int counter;
	private float changeDirection;	// Float to saying when (whisper) should change direction
	private float speed;			// Speed of (whisper)
	private Vector3 vel;			// Velocity of (whisper)
	private bool reverseAudioDirection;	// Reverse when too far away from the player;

	// Player used to transfer scenes and play audio.


	// Use this for initialization
	void Start () {
		timeAllot = 3f; // Teleportation and Audio scripts will be called after (timeAllot) seconds.
		changeDirection = 1f; // Direction of 3D audio will change every (cD) seconds
		bool flash = false;
		audio.volume = .1f;

		player = GameObject.FindGameObjectWithTag ("Player");


		// counter = 0; // Counter for the whisperClips.

		// Handles Moving Audio
		is3DAudioPlaying = false;
		reverseAudioDirection = false;
		speed = 4f;
		vel = Random.insideUnitSphere * speed;
	}
	
	// Update is called once per frame
	void Update () {
		if (flash) { // If The text on the screen should be flashing, flash and play the flicker audio.
			float messageDet = Random.value;
			Light [] lights = transform.parent.GetComponentsInChildren<Light> ();
			// Channge the messages that appear on the screen when Triggered.
			if (messageDet < .04) {
				screenText.GetComponent<TextMesh>().text = messages[1];
				foreach (Light l in lights) {
					l.enabled = false;
				}
			}
			else {
				screenText.GetComponent<TextMesh>().text = messages[0];
				foreach (Light l in lights) {
					l.enabled = true;
				}
			}
			timeAllot -= Time.deltaTime;

			// Play "flicker" sound.
			if (!audio.isPlaying) {
				audio.clip = flicker;
				audio.Play ();
			}

			// Determine when to destroy
			if (timeAllot < 0f) { 
				flash = false;
				timeAllot = 3f;
				Destroy (screenText);
				audio.Stop();
				foreach (Light l in lights) {
					l.enabled = false;
				}
			}
		}

		// Move Sound.
		if (is3DAudioPlaying) {
			// Flash lights in Halluroom
			FlashHalluLight();

			// Check to see if we need to change the direction of the audio (every 1 second)
			if (changeDirection > 0) {
				changeDirection -= Time.deltaTime;
			}
			else {
				changeDirection = 1f;
				vel = Random.insideUnitSphere * speed;
				vel.y = 0;
			}

			// Check to see if we need to REVERSE the direction of the audio
			if (reverseAudioDirection) {
				changeDirection = 1f;
				reverseAudioDirection = false;
				vel = vel * -1f;
				vel.y = 0;
			}

			// Move the audio, determining if the player is really far away or not.
			if (Vector3.Distance(player.transform.position, whisper.transform.position) < 10) {
				whisper.transform.Translate(vel * Time.deltaTime);
			}

			else {
				Debug.Log ("Moving to player");
				whisper.transform.position = Vector3.MoveTowards(whisper.transform.position, player.transform.position, Time.deltaTime * speed);
			}

			if (!whisper.audio.isPlaying) {
				FinishCutscene ();
			}
		}
	}

	// Begins the cutscene
	public void Begin(AudioClip journalClip) {
		Debug.Log ("Begin Called");
		whisperClip = journalClip;
		FlashText ();
	}

	void OnTriggerEnter(Collider col) {
		if (col.name == "First Person Controller")
			FlashText ();
	}

	// I suppose FlashText isn't vialbe anymore. This starts the functions and sets up the puzzle.
	// FlashText Flashes text on the screen, then afterwards calls the next part of this hallucination engine.
	void FlashText() {
		// Actual Flashing of the text
		Debug.Log ("Flash the text!");
		mc = GameObject.FindGameObjectWithTag ("MainCamera");
		screenText = (GameObject)Instantiate(textPrefab, new Vector3(mc.transform.position.x, mc.transform.position.y, mc.transform.position.z), Quaternion.identity);
		screenText.transform.parent = GameObject.FindGameObjectWithTag ("MainCamera").transform;
		screenText.transform.localRotation = Quaternion.Euler(mc.transform.localRotation.x, mc.transform.localRotation.y, mc.transform.localRotation.z);
		screenText.GetComponent<TextMesh>().fontSize = 120;
		screenText.GetComponent<TextMesh>().offsetZ = 70;
		screenText.GetComponent<TextMesh>().text = messages[0];
		flash = true; // Set flashing flag to true

		// Removes fog for pitch-blackness.
		RenderSettings.fog = false;

		// Invoke functions after alloted time to continue with the Cutscene.
		Invoke("RemoveLights", timeAllot); // Move on to the next part in (3) seconds.
		Invoke("BeginAudio", timeAllot); // Plays Creepy 2D Sound
		Invoke("Play3DAudio", 0f); // Plays 3D Whisper/Text
		Invoke ("MovePlayer", timeAllot);
	}

	// Moves Player to the designated location;
	void MovePlayer() {
		Debug.Log ("Move Player");
		initialPosition = player.transform.position;
		player.transform.position = GameObject.Find ("Teleport George").transform.position;
		whisper.transform.position = player.transform.position;
	}

	// Flashes Light in Halluroom.
	void FlashHalluLight() {
		foreach (Light l in halluroomLights) {
			float flashLightDet = Random.value;
			if (flashLightDet < .04) {
				l.enabled = false;
			}

			else if (disableFlashing < 0) {
				l.enabled = true;
				disableFlashing = 1f;
			}
		}
		disableFlashing -= Time.deltaTime;
	}

	// Removes Lights from the parent making it darker.
	void RemoveLights() {
		Light [] lights = transform.parent.GetComponentsInChildren<Light> ();
		MeshRenderer [] mr = transform.parent.GetComponentsInChildren<MeshRenderer>();
		Debug.Log ("Num lights = " + lights.Length);

		foreach (Light l in lights) {
			l.enabled = false;
		}

		// Also Removes material from Lamp shader.
		foreach (MeshRenderer m in mr) {
			foreach (Material mat in m.materials) {
				mat.shader = Shader.Find ("Diffuse");
			}
		}
	}

	void AddLights() {
		Light [] lights = transform.parent.GetComponentsInChildren<Light> ();
		Debug.Log ("Num lights = " + lights.Length);
		
		foreach (Light l in lights) {
			l.enabled = true;
		}
	}

	void BeginAudio() {
		// Play 2D Sound for BG Noise
		audio.volume = 1f;
		audio.clip = noise;
		audio.Play ();

		}

	void Play3DAudio() {
		// Instantiate Whsiper GO to a location nearby the player.
		Debug.Log ("Create 3D Audio GO");
		whisper = (GameObject)Instantiate (new GameObject ());
		mc = GameObject.FindGameObjectWithTag ("MainCamera");

		whisper.transform.position = new Vector3 (mc.transform.position.x + 2, mc.transform.position.y, mc.transform.position.z + 2);

		// Add Audio Source for whisper
		whisper.AddComponent<AudioSource> ();
		whisper.audio.clip = whisperClip;
		whisper.audio.loop = false;
		whisper.audio.Play ();

		// Add box collider for whisper
		whisper.AddComponent<BoxCollider> ();
		whisper.GetComponent<BoxCollider> ().isTrigger = true;

		// Add TriggerScript for whisper
		whisper.AddComponent<CutsceneGeorge3DTrigger> ();

		// 3D Audio is playing to true
		is3DAudioPlaying = true;
	}

	public void ReposAudio() {
		Debug.Log ("Repos Audio");
		/*counter += 1;
		Destroy (whisper);
		if (counter < whisperClips.Length) {
			Play3DAudio ();
		}*/
		float speed = 10f;
		Vector3 vel = Random.insideUnitSphere * speed;
		vel.y = 0;
		transform.Translate (vel * Time.deltaTime);
	}

	public void ReverseAudio() {
		reverseAudioDirection = true;
	}

	void FinishCutscene() {
		// Add Lights back to scene and Stop 2D sound.
		Destroy (whisper);
		AddLights ();
		audio.Stop ();
		flash = false;
		is3DAudioPlaying = false;
		player.transform.position = spawn.transform.position;
		RenderSettings.fog = true;
	}
}
