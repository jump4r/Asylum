using UnityEngine;
using System.Collections;

public class JournalManager : MonoBehaviour {
	Journal[] journals; // List of Inmates, for their respective states, importable by the
	
	// Use this for initialization
	void Start () {
		journals = new Journal[6];
		journals[0] = GameObject.FindGameObjectWithTag("Journal1").GetComponent<Journal>();
		journals[1] = GameObject.FindGameObjectWithTag("Journal2").GetComponent<Journal>();
		journals[2] = GameObject.FindGameObjectWithTag("Journal3").GetComponent<Journal>();
		journals[3] = GameObject.FindGameObjectWithTag("Journal4").GetComponent<Journal>();
		journals[4] = GameObject.FindGameObjectWithTag("Journal5").GetComponent<Journal>();
		journals[5] = GameObject.FindGameObjectWithTag("Journal6").GetComponent<Journal>();

		showJournals ();
		placeJournals ();
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	// Determines whether ANY journal is playing.
	public bool isJournalPlaying() {
		foreach (Journal j in journals) {
			if (j.audio.isPlaying) {
				return true;
			}
		}
		return false;
	}

	public void hideJournals() {
		foreach (Journal j in journals) {
			j.gameObject.GetComponent<MeshRenderer>().enabled = false;
		}
	}

	public void showJournals() {
		foreach (Journal j in journals) {
			j.gameObject.GetComponent<MeshRenderer>().enabled = true;
		}
	}

	public void placeJournals() {
		foreach (Journal j in journals) {
			j.transform.localPosition = j.journal_locs[j.current_journal];
		}
	}

	string[] ParsePreq(TextAsset ta) {	// This like wasn't even needed now that I took all the shit out of it but i'm keeping it anyway fight me about it.
		string[] preq = ta.text.Split ("\n"[0]);
		return preq;
	}

	// Determine whether the pre-req of a certain conversation has been met or not based on all of the other Journals.
	public bool ValidConversation(TextAsset ta) {
		string[] preq = ParsePreq (ta);
		string[] person = new string[preq.Length];	// Inmate name
		int[] nums = new int[preq.Length];			// Number conversation required
		
		for (int i = 0; i < preq.Length; i++) { // Put the respective 
			string[] words = preq[i].Split (' ');
			person[i] = words[0];
			nums[i] = int.Parse (words[1]);
			//Debug.Log (person[i] + " and " + nums[i]);
		}
		
		for (int j = 0; j < journals.Length; j++) {
			if (nums[j] > journals[j].GetCurrentJournal ()) {
				Debug.Log ("Prerequisite not met!");
				return false; // If the Preq number is GREATER than the Current conversation number, the preq hasn't been met
			}
		}
		Debug.Log ("Prerequisite met!"); 
		return true; // If no Preq Number detected that is greater than the current conv number
	}
}
