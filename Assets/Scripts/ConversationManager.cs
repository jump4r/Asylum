using UnityEngine;
using System.Collections;

public class ConversationManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col) {
		if (col.name == "First Person Controller") { // Find associated trigger, called to whichever AI this conv belongs to, and call the conversation.
			Debug.Log ("Conversation Started");
			Conversation conv = GameObject.FindGameObjectWithTag("Person1").GetComponent<Conversation>();
			conv.Setup ("Person1"); // In actuallity, this will find the tag of the Triggerbox's AI.
			conv.TriggerConversation();
		}
	}
}
