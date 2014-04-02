using UnityEngine;
using System.Collections;

public class JournalManager : MonoBehaviour {
	Journal[] journals; // List of Inmates, for their respective states, importable by the 
	
	// Use this for initialization
	void Start () {
		journals = new Journal[3];
		journals[0] = GameObject.FindGameObjectWithTag("Journal1").GetComponent<Journal>();
		journals[1] = GameObject.FindGameObjectWithTag("Journal2").GetComponent<Journal>();
		journals[2] = GameObject.FindGameObjectWithTag("Journal3").GetComponent<Journal>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	if (journals[0].GetCurrentJournal () != 0) {
	//		
		}
	}
	
	string[] ParsePreq(TextAsset ta) {	// This like wasn't even needed now that I took all the shit out of it but i'm keeping it anyway fight me about it.
		string[] preq = ta.text.Split ("\n"[0]);
		return preq;
	}
	
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
