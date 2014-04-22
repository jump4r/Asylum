using UnityEngine;
using System.Collections;

// All functions and elements involved in George's Cutscene.
public class CutsceneGeorge : MonoBehaviour {

	public GameObject textPrefab; // Prefab of TextMesh.
	public AudioClip noise;
	public AudioClip flicker;
	public AudioClip[] creepyClips;
	public string[] messages;

	// Handles Text Flashing
	private GameObject screenText; // text that will flash on the screen
	private bool flash;	// Flash the text in Update?
	private float timeAllot; // allotted time for flashing text

	// Handles 3D Audio Playing
	public AudioClip[] whisperClips;
	private GameObject whisper;

	private bool is3DAudioPlaying;
	private GameObject mc;
	private int counter;
	private float changeDirection;
	private float speed;
	private Vector3 vel;

	// Use this for initialization
	void Start () {
		timeAllot = 3f; // Teleportation and Audio scripts will be called after (timeAllot) seconds.
		changeDirection = 1f; // Direction of 3D audio will change every (cD) seconds
		bool flash = false;
		audio.volume = .1f;

		counter = 0; // Counter for the whisperClips.

		is3DAudioPlaying = false;
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
			if (changeDirection > 0) {
				changeDirection -= Time.deltaTime;
			}
			else {
				changeDirection = 1f;
				vel = Random.insideUnitSphere * speed;
				vel.y = 0;
			}
			whisper.transform.Translate(vel * Time.deltaTime);
		}
	}

	void OnTriggerEnter(Collider col) {
		if (col.name == "First Person Controller")
			FlashText ();
	}

	// FlashText Flashes text on the screen, then afterwards calls the next part of this hallucination engine.
	void FlashText() {
		Debug.Log ("Flash the text!");
		mc = GameObject.FindGameObjectWithTag ("MainCamera");
		screenText = (GameObject)Instantiate(textPrefab, new Vector3(mc.transform.position.x, mc.transform.position.y, mc.transform.position.z), Quaternion.identity);
		screenText.transform.parent = GameObject.FindGameObjectWithTag ("MainCamera").transform;
		screenText.transform.localRotation = Quaternion.Euler(mc.transform.localRotation.x, mc.transform.localRotation.y, mc.transform.localRotation.z);
		screenText.GetComponent<TextMesh>().fontSize = 120;
		screenText.GetComponent<TextMesh>().offsetZ = 70;
		screenText.GetComponent<TextMesh>().text = messages[0];
		flash = true; // Set flashing flag to true

		Invoke("RemoveLights", timeAllot); // Move on to the next part in (3) seconds.
		Invoke("BeginAudio", timeAllot + 1f); // Plays Creepy 2D Sound
		Invoke("Play3DAudio", timeAllot + 1f); // Plays 3D Whisper/Text
		Invoke ("MovePlayer", timeAllot + .5f);
	}

	// Moves Player to the designated location;
	void MovePlayer() {
		Debug.Log ("Move Player");
		GameObject player = GameObject.Find ("First Person Controller");
		player.transform.position = GameObject.Find ("Teleport George").transform.position;
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
		if (counter < whisperClips.Length) {
			// Instantiate Whsiper GO to a location nearby the player.
			Debug.Log ("Create 3D Audio GO");
			whisper = (GameObject)Instantiate (new GameObject ());
			mc = GameObject.FindGameObjectWithTag ("MainCamera");
			whisper.transform.position = new Vector3 (mc.transform.position.x + 2, mc.transform.position.y, mc.transform.position.z + 2);

			// Add Audio Source for whisper
			whisper.AddComponent<AudioSource> ();
			whisper.audio.clip = whisperClips [counter];
			whisper.audio.loop = true;
			whisper.audio.Play ();

			// Add box collider for whisper
			// whisper.AddComponent<BoxCollider> ();
			// whisper.GetComponent<BoxCollider> ().isTrigger = true;

			// Add TriggerScript for whisper
			// whisper.AddComponent<CutsceneGeorge3DTrigger> ();

			// 3D Audio is playing to true
			is3DAudioPlaying = true;
		} 

		else {
			FinishCutscene();
		}
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

	void FinishCutscene() {
		// Add Lights back to scene and Stop 2D sound.
		AddLights ();
		audio.Stop ();
	}
}
