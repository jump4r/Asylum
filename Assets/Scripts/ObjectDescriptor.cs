using UnityEngine;
using System.Collections;

public class ObjectDescriptor : MonoBehaviour {

	public TextAsset desc;
	// public AudioClip clip;
	private bool active_GUI = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
	void OnTriggerEnter(Collider col) {
		if (col.name == "First Person Controller") {
			//Debug.Log("Player Collision Detected");
			active_GUI = true;
		}
	}
	
	void OnTriggerExit(Collider col) {
		//Debug.Log (col.name);
		if (col.name  == "First Person Controller") {
			active_GUI = false;
		}
	}
	
	void DisplayText () {
		
	}
	
	void OnGUI() {
		if (active_GUI) {		
//			Debug.Log (desc);
			GUI.Box (new Rect(Screen.width * .3f, Screen.height * .9f, Screen.width *.4f, 40f), desc.ToString ());
		}
	}
}
