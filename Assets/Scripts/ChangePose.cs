using UnityEngine;
using System.Collections;

public class ChangePose : MonoBehaviour {

	public GameObject[] poses;
	public string name;

	private int index;
	private CharacterManager cm;
	// Use this for initialization
	void Start () {
		cm = GameObject.FindGameObjectWithTag ("GameController").GetComponent<CharacterManager> ();
		index = 0;
		ReposeCharacter ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Change the character position.
	void ReposeCharacter() {
		if (cm.IsActive (name)) {
			poses[index].SetActive(false);
			index = (index + 1) % poses.Length;
			poses[index].SetActive (true);
			Debug.Log ("Character Activated and Set! Index = " + index);
		}
	}

	void OnTriggerExit() {
		// cm.ActivateCharacter (name);
		ReposeCharacter ();
	}
}
