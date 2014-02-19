using UnityEngine;
using System.Collections;

public class Inmate : MonoBehaviour {

	public AudioClip[] conv_clips;
	public TextAsset[] conv_texts;
	public string[] state = new string[2];

	private string in_name;
	private int current_conv;

	// Use this for initialization
	void Start () {
		// Declare AI States.
		state [0] = "idle";
		state [1] = "talking";
		current_conv = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void FinishConv() {
		current_conv += 1; // Increase the conversation counter.
	}

	public int GetCurrentConv() {
		return current_conv;
	}
	
}
