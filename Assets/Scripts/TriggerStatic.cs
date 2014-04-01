using UnityEngine;
using System.Collections;

public class TriggerStatic : MonoBehaviour {
	public AudioClip staticAudio;
	private bool triggered;
	// Use this for initialization
	void Start () {
		triggered = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!audio.isPlaying) {
			Debug.Log ("No audio");
		}
	}

	void OnTriggerEnter(Collider Col) {
		if (!triggered) {
			Debug.Log ("static spooky sound played");
			AudioSource.PlayClipAtPoint (staticAudio, transform.position);
			triggered = true;
		}
	}
}
