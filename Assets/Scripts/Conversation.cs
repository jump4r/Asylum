using UnityEngine;
using System.Collections;

public class Conversation : MonoBehaviour{

	public AudioClip conv_clip; // The audio for the clip
	public TextAsset conv_text; // The text for the clip

	private bool active_speech;

	private int sub_switch;	// Used for subtitles.
	private string current_sub;
	// Use this for initialization

	void Start () {
		active_speech = false;
		sub_switch = 0;
	}

	public void Setup(string tag) {	// Setup with the correct conversation based off of the tag.
		Inmate inm = GameObject.FindGameObjectWithTag (tag).GetComponent<Inmate> ();
		Debug.Log (inm.GetCurrentConv ());
		if (inm.GetCurrentConv () >= inm.conv_clips.Length) { // If out of bounds, set the clips to 0, as that will be the default.
			conv_clip = inm.conv_clips [0];
			conv_text = inm.conv_texts [0];
		} 
		else {
			conv_clip = inm.conv_clips [inm.GetCurrentConv ()];
			conv_text = inm.conv_texts [inm.GetCurrentConv ()];
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (active_speech && !audio.isPlaying) { // If talking, Determine when to switch the subtitles.
			active_speech = false;
			EndConversation();
		} 
		else if (active_speech) {
			// Get subtitles to rotate with text, I will do this in class (2-19).
		}
	}

	public void TriggerConversation() {	// If the player collides with the zone, for now, start the audio.
		Debug.Log ("Conversation Triggered from ConvManager Script.");
		active_speech = true;
		if (!audio.isPlaying) {
			audio.clip = conv_clip;
			audio.Play();
		}
	}

	void EndConversation() {
		GameObject.FindGameObjectWithTag (tag).GetComponent<Inmate> ().FinishConv ();
	}

	void OnGUI() {
		if (active_speech) {
			current_sub = conv_text.ToString();
			GUI.Box (new Rect(Screen.width * .3f, Screen.height * .9f, Screen.width *.4f, 40f), current_sub);
		}
	}
}
