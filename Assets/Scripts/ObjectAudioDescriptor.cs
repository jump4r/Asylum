using UnityEngine;
using System.Collections;

public class ObjectAudioDescriptor : MonoBehaviour {
	
	public TextAsset desc;
	// public AudioClip clip;
	private bool active_GUI = false;
	private bool active_playing = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (active_playing) {
			if (!audio.isPlaying) { // if the active_playing is set and audio is not playing
				Finish ();
			}
		}
	}

	void OnTriggerEnter(Collider col) {
		if (col.name == "First Person Controller" && !audio.isPlaying) {
			//Debug.Log("Player Collision Detected");
			active_GUI = true;
			active_playing = true;
			audio.Play();
		}
	}
	
	void Finish() {
		active_playing = false;
		active_GUI = false;
	}

	void OnGUI() {
		if (active_GUI) {		
			//Debug.Log (desc);
			GUI.Box (new Rect(Screen.width * .3f, Screen.height * .9f, Screen.width *.4f, 40f), desc.ToString ());
		}
	}
}


