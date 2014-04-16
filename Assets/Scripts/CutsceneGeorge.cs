using UnityEngine;
using System.Collections;

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

	private GameObject mc;
	private int counter;

	// Use this for initialization
	void Start () {
		timeAllot = 3f;
		bool flash = false;
		audio.volume = .1f;
		counter = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (flash) { // If The text on the screen should be flashing, flash and play the flicker audio.
			float messageDet = Random.value;
			Light [] lights = transform.parent.GetComponentsInChildren<Light> ();
			if (messageDet < .04) {
				screenText.GetComponent<TextMesh>().text = messages[1];
				Debug.Log ("Test1");
				foreach (Light l in lights) {
					l.enabled = false;
				}
			}
			else {
				screenText.GetComponent<TextMesh>().text = messages[0];
				Debug.Log ("Test2");
				foreach (Light l in lights) {
					l.enabled = true;
				}
			}
			Debug.Log (messageDet);
			timeAllot -= Time.deltaTime;

			if (!audio.isPlaying) {
				audio.clip = flicker;
				audio.Play ();
			}

			if (timeAllot < 0f) { // Determine when to destroy
				flash = false;
				timeAllot = 3f;
				Destroy (screenText);
				audio.Stop();
			}
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
		Invoke("BeginAudio", timeAllot); // Plays Creepy 2D Sound
		Invoke("Play3DAudio", timeAllot); // Plays 3D Whisper/Text
	}

	// Removes Lights from the parent making it darker.
	void RemoveLights() {
		Light [] lights = transform.parent.GetComponentsInChildren<Light> ();
		Debug.Log ("Num lights = " + lights.Length);

		foreach (Light l in lights) {
			l.enabled = false;
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
			whisper.AddComponent<BoxCollider> ();
			whisper.GetComponent<BoxCollider> ().isTrigger = true;

			// Add TriggerScript for whisper
			whisper.AddComponent<CutsceneGeorge3DTrigger>();
		}
	}

	public void ReposAudio() {
		counter += 1;
		Destroy (whisper);
		if (counter < whisperClips.Length) {
			Play3DAudio ();
		}
	}

	void FinishCutscene() {
		// Add Lights back to scene and Stop 2D sound.
		AddLights ();
		audio.Stop ();
	}
}
