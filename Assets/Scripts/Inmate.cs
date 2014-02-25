using UnityEngine;
using System.Collections;

public class Inmate : MonoBehaviour {

	public AudioClip[] conv_clips;
	public TextAsset[] conv_texts;
	public string in_name;

	[HideInInspector]
	public string[] state;
	public Vector3[] conv_locs;

	private int current_conv;
	private int current_state;
	// Use this for initialization
	void Start () {

		// Declare AI States.
		state [0] = "idle";
		state [1] = "talking";

		// Declare Conversation Locations
		conv_locs = new Vector3[2];
		conv_locs[0] = new Vector3(-1.19f, 4.7f, -12.3f); // Starting Position
		conv_locs[1] = new Vector3(-1.19f, 4.7f, -22.3f); // Position after first conversation.
		// -1.19f, 4.7f, -12.3f
		// Current Conversation Index.
		current_conv = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void FinishConv() {
		current_conv += 1; // Increase the conversation counter.
		current_state = 0; // Set state to idle;
	}

	public int GetCurrentConv() {
		return current_conv;
	}
	
}
