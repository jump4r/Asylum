using UnityEngine;
using System.Collections;

public class CutsceneGeorge : MonoBehaviour {

	public GameObject textPrefab; // Prefab of TextMesh.
	public AudioClip noise;
	public AudioClip flicker;
	public AudioClip[] creepy;
	public string[] messages;

	// Handles Text Flashing
	private GameObject screenText; // text that will flash on the screen
	private bool flash;	// Flash the text in Update?
	private float timeAllot; // allotted time for flashing text

	// Use this for initialization
	void Start () {
		timeAllot = 3f;
		bool flash = false;
		audio.volume = .1f;
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
		GameObject mc = GameObject.FindGameObjectWithTag ("MainCamera");
		screenText = (GameObject)Instantiate(textPrefab, new Vector3(mc.transform.position.x, mc.transform.position.y, mc.transform.position.z), Quaternion.identity);
		screenText.transform.parent = GameObject.FindGameObjectWithTag ("MainCamera").transform;
		screenText.transform.localRotation = Quaternion.Euler(mc.transform.localRotation.x, mc.transform.localRotation.y, mc.transform.localRotation.z);
		screenText.GetComponent<TextMesh>().fontSize = 120;
		screenText.GetComponent<TextMesh>().offsetZ = 70;
		screenText.GetComponent<TextMesh>().text = messages[0];
		flash = true; // Set flashing flag to true

		Invoke("RemoveLights", timeAllot); // Move on to the next part in (3) seconds.
		Invoke("BeginAudio", timeAllot); // Plays Creepy 2D and 3D sounds.
	}

	// Removes Lights from the parent making it darker.
	void RemoveLights() {
		Light [] lights = transform.parent.GetComponentsInChildren<Light> ();
		Debug.Log ("Num lights = " + lights.Length);

		foreach (Light l in lights) {
			l.enabled = false;
		}
	}

	void BeginAudio() {
		// Play 2D Sound for BG Noise
		audio.volume = 1f;
		audio.clip = noise;
		audio.Play ();

		// Play 3D Sound for 


		}
}
