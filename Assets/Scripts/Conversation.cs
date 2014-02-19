using UnityEngine;
using System.Collections;

public class Conversation : MonoBehaviour {
	public AudioClip conv_clip;
	public TextAsset conv_text;
	private bool active_speech;
	private int sub_switch;
	private string current_sub;
	// Use this for initialization
	void Start () {
		active_speech = false;
		sub_switch = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (active_speech && !audio.isPlaying) { // If talking, Determine when to switch the subtitles.
			active_speech = false;
		} 
		else if (active_speech) {
			// Get subtitles to rotate with text, do this in class.
		}
	}

	public void TriggerConversation() {	// If the player collides with the zone, for now, start the audio.
		// Debug.Log ("Conversation Triggered from ConvScript.");
		active_speech = true;
		if (!audio.isPlaying) {
			audio.clip = conv_clip;
			audio.Play();
		}
	}

	void OnGUI() {
		if (active_speech) {
			current_sub = conv_text.ToString();
			GUI.Box (new Rect(Screen.width * .3f, Screen.height * .9f, Screen.width *.4f, 40f), current_sub);
		}
	}
}
