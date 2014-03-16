using UnityEngine;
using System.Collections;

public class TriggerObjAudioGUI : MonoBehaviour {

	private bool active_GUI = false;
	private bool active_playing = false;
	//private ObjectDescriptor child_script = GetComponentInChildren<ObjectDescriptor>();
	AudioSource aSource;
	ObjectDescriptor child_script;

	// Use this for initialization
	void Start () {
		aSource = GetComponentInChildren<AudioSource>();
		child_script = GetComponentInChildren<ObjectDescriptor>(); // Get the 
	}
	
	// Update is called once per frame
	void Update () {
		if (active_playing) {
				if (!aSource.audio.isPlaying) { // if the active_playing is set and audio is not playing
					Finish ();
			}
		}
	}

	void OnTriggerEnter(Collider col) {
		if (col.name == "First Person Controller" && !aSource.audio.isPlaying) {
			Debug.Log("Player Collision Detected");
			active_GUI = true;
			active_playing = true;
			aSource.audio.Play();
		}
	}

	void OnTriggerExit(Collider col) {
		Debug.Log (col.name);
	}

	void Finish() {
		active_playing = false;
		active_GUI = false;
	}

	void OnGUI() {
		if (active_GUI) {
			string desc = child_script.desc.ToString();

			//Debug.Log (desc);
			GUI.Box (new Rect(Screen.width * .3f, Screen.height * .9f, Screen.width *.4f, 40f), desc);
		}
	}
}
