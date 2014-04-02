using UnityEngine;
using System.Collections;

public class Journal : MonoBehaviour {

	// List of journals left behind by the Inmates. Current_journal represents the number of journals traversed.
	public AudioClip[] journal_clips;
	public TextAsset[] journal_texts;
	public TextAsset[] journal_preqs;
	public Vector3[] journal_locs;

	[HideInInspector]
	public string[] state;

	private int current_journal;
	// Use this for initialization
	void Start () {
		current_journal = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void FinishRead() {
		transform.localPosition = journal_locs [current_journal];
		current_journal += 1; // Increase the conversation counter.
		Debug.Log ("Journal " + name + "'s current conv is " + current_journal);

	}

	public int GetCurrentJournal() {
		return current_journal;
	}

	void OnTriggerEnter(Collider col) {
		if (col.name == "First Person Controller" || col.name == "OVRPlayerController") { // Find associated trigger, called to whichever AI this conv belongs to, and call the conversation.
			// Debug.Log ("Conversation Started");
			string sub_name = name.Substring (0, Mathf.Min (name.Length, 7));	// Gets the name of the Trigger, which in term determines what Inmate it belongs to
			// Debug.Log (sub_name);	// All inmates will have names Person1, Person2, etc, along with their regular names. Actually, I don't think this really matters.
			JournalRead jr = GetComponent<JournalRead>();
			jr.Setup (sub_name); // In actuallity, this will find the tag of the Triggerbox's AI. Sets up the conversation with the AI
		}
	}
}
