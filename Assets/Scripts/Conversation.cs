using UnityEngine;
using System.Collections;

public class Conversation : MonoBehaviour{
	
	public AudioClip conv_clip; // The audio for the clip
	public TextAsset conv_text; // The text for the clip
	
	[HideInInspector]
	public bool valid; // is valid conversation, or use default speech. Needs to be public so I can access in ConvTrigger.
	
	private bool active_speech;
	private float sub_switch;	// Used for subtitles.
	private int current_sub;
	private string[] subtitle;
	private bool print_two_lines;
	// Use this for initialization
	private ConversationManager cm;
	
	void Start () {
		cm = GameObject.FindGameObjectWithTag("GameController").GetComponent<ConversationManager> ();
		active_speech = false;
		sub_switch = 0;
		current_sub = 0;
	}
	
	public void Setup(string tag) {	// Setup with the correct conversation based off of the tag.
		Inmate inm = GameObject.FindGameObjectWithTag (tag).GetComponent<Inmate> ();
		sub_switch = 0;
		current_sub = 0;
		print_two_lines = true;
		//Debug.Log ("The tag is: " + tag + "\n" + inm.name + "'s Current Conv is " + inm.GetCurrentConv ());
		//Debug.Log ("The conv_preqs length is " + inm.conv_preqs.Length);
		
		// Determine if the conversation is valid
		valid = cm.ValidConversation (inm.conv_preqs [inm.GetCurrentConv ()]);
		if (valid) {
			// If out of bounds, set the clips to 0, as that will be the default.
			if (inm.GetCurrentConv () >= inm.conv_clips.Length) {
				conv_clip = inm.conv_clips [0];
				conv_text = inm.conv_texts [0];
			} 
			
			else {
				conv_clip = inm.conv_clips [inm.GetCurrentConv ()];
				conv_text = inm.conv_texts [inm.GetCurrentConv ()];
			}
			
			// Split raw txt into lines for parsing.
			subtitle = conv_text.text.Split ("\n" [0]);
			if (subtitle.Length <= 1) {
				print_two_lines = false;
			}
			
			// Trigger the Conversation if Valid
			TriggerConversation();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (active_speech && !audio.isPlaying) { // If talking, Determine when to switch the subtitles.
			active_speech = false;
			EndConversation();
		} 
		
		else if (active_speech) {	
			sub_switch += Time.deltaTime;	// Determine when to switch subtitles.
			if (sub_switch > 5.35 && current_sub < subtitle.Length-2) { // Literally the most editable number in the game.
				sub_switch = 0;
				current_sub += 2;
				if (current_sub >= subtitle.Length)
					print_two_lines = false;
			}
		}
	}
	
	public void TriggerConversation() {	// If the player collides with the zone, for now, start the audio.
		// Debug.Log ("Conversation Triggered from ConvManager Script.");
		active_speech = true;
		if (!audio.isPlaying) {
			audio.clip = conv_clip;
			audio.Play();
		}
	}
	
	void EndConversation() {
		if (valid) {
			GameObject.FindGameObjectWithTag (tag).GetComponent<Inmate> ().FinishConv ();
		}
	}
	
	void OnGUI() {
		if (active_speech) {
			//current_sub = conv_text.ToString();
			if (print_two_lines)
				GUI.Box (new Rect(Screen.width * .3f, Screen.height * .9f, Screen.width *.4f, 40f), subtitle[current_sub] + "\n" + subtitle[current_sub+1]);
			else 
				GUI.Box (new Rect(Screen.width * .3f, Screen.height * .9f, Screen.width *.4f, 40f), subtitle[current_sub]);
		}
	}
}
