using UnityEngine;
using System.Collections;

public class ConversationTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col) {
		if (col.name == "First Person Controller") { // Find associated trigger, called to whichever AI this conv belongs to, and call the conversation.
			// Debug.Log ("Conversation Started");
			string sub_name = name.Substring (0, Mathf.Min (name.Length, 7));
			Debug.Log (sub_name);
			Conversation conv = GameObject.FindGameObjectWithTag(sub_name).GetComponent<Conversation>();
			Inmate inm = GameObject.FindGameObjectWithTag(sub_name).GetComponent<Inmate>();
			conv.Setup (sub_name); // In actuallity, this will find the tag of the Triggerbox's AI. Sets up the conversation with the AI
			conv.TriggerConversation();	// 
			if (inm.GetCurrentConv()+1 < inm.conv_locs.Length) {
				MoveTrigger (inm.conv_locs[inm.GetCurrentConv ()+1]);	// Move the Trigger to the next Conversation Location.
           }
		}
	}

	void MoveTrigger(Vector3 new_pos) {
		transform.localPosition = new Vector3 (new_pos.x, new_pos.y, new_pos.z);
	}
}
