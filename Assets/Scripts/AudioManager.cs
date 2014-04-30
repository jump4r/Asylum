using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	public Journal journalPlayed;

	// Journal Audio & Text to play in different scenes.
	private AudioClip journalClip;
	private TextAsset journalText;
	
	private bool changeLevel;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (transform.gameObject); // Bring the AudioManager 
		changeLevel = false; // Determines when to change the level on audioExit.
	}
	
	// Update is called once per frame
	void Update () {
		// Determine's when to change the level
		/* if (Application.loadedLevel != 1) {
			if (!journalPlayed.gameObject.audio.isPlaying) {
				Destroy (transform.gameObject);
				Application.LoadLevel ("asylum-test");
			}
		}
		Debug.Log (Application.loadedLevel); */
	}

	// Plays the audio
	public void PlayAudio(AudioClip jClip) {
		journalClip = jClip;
		audio.clip = journalClip;
		audio.Play ();
		changeLevel = true;
	}
}
