using UnityEngine;
using System.Collections;

public class JournalRead : MonoBehaviour{
	
	public AudioClip journal_clip; // The audio for the clip
	public TextAsset journal_text; // The text for the clip
	public GameObject textPrefab; // Prefab of TextMesh.
	[HideInInspector]
	public bool valid; // is valid conversation, or use default speech. Needs to be public so I can access in ConvTrigger.
	
	private bool active_speech;
	private float sub_switch;	// Used for subtitles.
	private int current_sub;
	private string[] subtitle;
	private bool print_two_lines;
	// Use this for initialization
	private JournalManager jm;
	
	void Start () {
		jm = GameObject.Find ("GameVariables").GetComponent<JournalManager> ();
		active_speech = false;
		sub_switch = 0;
		current_sub = 0;
	}
	
	public void Setup(string tag) {	// Setup with the correct conversation based off of the tag.
		Journal jour = GetComponent<Journal> ();
		sub_switch = 0;
		current_sub = 0;
		print_two_lines = true;
		//Debug.Log ("The tag is: " + tag + "\n" + inm.name + "'s Current Conv is " + inm.GetCurrentConv ());
		//Debug.Log ("The conv_preqs length is " + inm.conv_preqs.Length);
		
		// Determine if the conversation is valid
		valid = jm.ValidConversation (jour.journal_preqs [jour.GetCurrentJournal ()]);
		if (valid && !jm.isJournalPlaying()) {
			// If out of bounds, set the clips to 0, as that will be the default.
			if (jour.GetCurrentJournal () >= jour.journal_clips.Length) {
				journal_clip = jour.journal_clips [0];
				journal_text = jour.journal_texts [0];
			} 
			
			else {
				journal_clip = jour.journal_clips [jour.GetCurrentJournal ()];
				journal_text = jour.journal_texts [jour.GetCurrentJournal ()];
			}
			
			// Split raw txt into lines for parsing.
			subtitle = journal_text.text.Split ("\n" [0]);
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
			audio.clip = journal_clip;
			audio.Play();
		}

		// Instatiiate 3D Text the the screen. Useful in Oculus.
		Debug.Log ("Trigger 3D Text");
		GameObject mc = GameObject.FindGameObjectWithTag ("MainCamera");
		GameObject screen_text = (GameObject)Instantiate(textPrefab, new Vector3(mc.transform.position.x, mc.transform.position.y, mc.transform.position.z), Quaternion.identity);

		// screen_text.transform.rotation = Quaternion.Euler(mc.transform.rotation.x, mc.transform.rotation.y, mc.transform.rotation.z);
		screen_text.transform.parent = GameObject.FindGameObjectWithTag ("MainCamera").transform;
		// GameObject.FindGameObjectWithTag ("MainCamera").transform.position = GameObject.FindGameObjectWithTag ("MainCamera").transform.parent.position;
		//screen_text.transform.localPosition = GameObject.FindGameObjectWithTag ("MainCamera").transform.localPosition;
		screen_text.transform.localRotation = Quaternion.Euler(mc.transform.localRotation.x, mc.transform.localRotation.y, mc.transform.localRotation.z);
		// screen_text.transform.localPposition = new Vector3 (screen_text.transform.Lposition.x, screen_text.transform.position.y, screen_text.transform.position.z - 30);
		//screen_text.transform.Rotate (0, 180, 0, Space.World);
		screen_text.GetComponent<TextMesh>().text = "Test Test";
		screen_text.GetComponent<TextMesh>().fontSize = 50;
		screen_text.GetComponent<TextMesh>().offsetZ = 18;
	}
	
	void EndConversation() {
		if (valid) {
			GetComponent<Journal>().FinishRead ();
		}
	}
	
	/* void OnGUI() {
		if (active_speech) {
			//current_sub = conv_text.ToString();
			// GUI.skin.label.fontSize = (int)(Screen.height * .1f);
			// GUIText.fontSize = (int)(Screen.height * .1f);
			if (print_two_lines)
				GUI.Box (new Rect(Screen.width * .3f, Screen.height * .65f, Screen.width *.4f, Screen.height * .05f), subtitle[current_sub] + "\n" + subtitle[current_sub+1]);
			else 
				GUI.Box (new Rect(Screen.width * .3f, Screen.height * .65f, Screen.width *.4f, Screen.height * .05f), subtitle[current_sub]);
		}
	} */
}
