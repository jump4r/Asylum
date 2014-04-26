using UnityEngine;
using System.Collections;

public class JournalRead : MonoBehaviour{
	
	public AudioClip journal_clip; // The audio for the clip
	public TextAsset journal_text; // The text for the clip
	public GameObject textPrefab; // Prefab of TextMesh.
	public CutsceneGeorge georgeCutscene; // CutsceneScript for GEORGE.


	[HideInInspector]
	public bool valid; // is valid conversation, or use default speech. Needs to be public so I can access in ConvTrigger.
	
	private bool active_speech;
	private bool spawn = false; // Determine if player needs to spawn after puzzle is done.
	private GameObject screen_text; // Subtitles that appears on the screen
	private float sub_switch;	// Used for subtitles.
	private int current_sub;	// The current index of the subtitle
	private string[] subtitle;	// Total Array of subtitle strings in the journal
	// Use this for initialization
	private JournalManager jm;
	private AudioManager aManager;
	
	void Start () {
		jm = GameObject.FindGameObjectWithTag("GameController").GetComponent<JournalManager> ();
		aManager = GameObject.FindGameObjectWithTag ("AudioManager").GetComponent<AudioManager> ();
		active_speech = false;
		sub_switch = 0;
		current_sub = 0;

		Debug.Log (name + " Should not destroy on load");
		DontDestroyOnLoad (transform.parent);
	}

	// Setup with the correct conversation based off of the tag.
	public void Setup(string tag) {
		Journal jour = GetComponent<Journal> ();
		sub_switch = 0;
		current_sub = 0;

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
			
			// Trigger the Conversation if Valid, depending on which Journal was selected

				TriggerConversation(jour);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (active_speech && !audio.isPlaying) { // If talking, Determine when to switch the subtitles.
			active_speech = false;
			EndConversation();
			if (spawn) {
				GameObject.FindGameObjectWithTag("Player").transform.position = GameObject.FindGameObjectWithTag("SpawnLoc").transform.position;
			}
		} 
		
		/*else if (active_speech) {	
			sub_switch += Time.deltaTime;	// Determine when to switch subtitles.
			if (sub_switch > 2.65 && current_sub < subtitle.Length-2) { // Literally the most editable number in the game.
				sub_switch = 0;
				current_sub += 1;
			}
			screen_text.GetComponent<TextMesh>().text = subtitle[current_sub].ToString();
		}*/
	}
	
	public void TriggerConversation(Journal jour) {	// If the player collides with the zone, for now, start the audio.
		// Debug.Log ("Conversation Triggered from ConvManager Script.");
		active_speech = true;

		// Play Audio
		if (!audio.isPlaying) {
			audio.clip = journal_clip;
			audio.mute = false;
			audio.Play ();
		}

		// (Andrew 'jsx' puzzle)
		// Hacky solution, but mute audio when a special journal is being played, because it will need to be a 3D sound.
		/*if (jour.tag == "Journal5") { // MATT's journal, used as a sub until George's is in the game.
			GameObject cutscene = GameObject.Find ("George Cutscene");
			cutscene.GetComponent<CutsceneGeorge> ().Begin (journal_clip);
			audio.mute = true;
		} */

		// MATT's journal, used as a sub until George's is in the game. (David's puzzle)
		if (jour.tag == "Journal2") {
			aManager.journalPlayed = jour;
			Application.LoadLevel ("thescene");
			EndConversation();
		} 

		// Padding Markus's Github stats (Markus' Puzzle)
		if (jour.tag == "Journal3") {
			GameObject.FindGameObjectWithTag ("GameController").GetComponent<TetrisManager>().startPuzzle(0);	
			spawn = true;
		}


		// #believe (Nick's Puzzle)
		if (jour.tag == "Journal4") {
			Application.LoadLevel ("memory-puzzle");
			jm.hideJournals ();
		}

		// Instatiiate 3D Text the the screen. Useful in Oculus.
		/*
		Debug.Log ("Trigger 3D Text");
		GameObject mc = GameObject.FindGameObjectWithTag ("MainCamera");
		screen_text = (GameObject)Instantiate(textPrefab, new Vector3(mc.transform.position.x, mc.transform.position.y - 25, mc.transform.position.z), Quaternion.identity);
		screen_text.transform.parent = GameObject.FindGameObjectWithTag ("MainCamera").transform;
		screen_text.transform.localRotation = Quaternion.Euler(mc.transform.localRotation.x, mc.transform.localRotation.y, mc.transform.localRotation.z);
		screen_text.GetComponent<TextMesh>().fontSize = 40;
		screen_text.GetComponent<TextMesh>().offsetZ = 70;
		*/
	}
	
	void EndConversation() {
		if (valid) {
			GetComponent<Journal>().current_journal += 1;
			GetComponent<Journal>().transform.localPosition = GetComponent<Journal>().journal_locs[GetComponent<Journal>().current_journal];
			Debug.Log ("Journal " + GetComponent<Journal>().name + "'s current conv is " + GetComponent<Journal>().current_journal);
			//Destroy (screen_text);
			spawn = false;
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
