using UnityEngine;
using System.Collections;

public class TriggerGUIOnObject : MonoBehaviour {

	private bool active_GUI = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col) {
		Debug.Log (col.name);
		if (col.name == "Player") {
			active_GUI = true;
		}
	}

	void OnTriggerExit(Collider col) {
		if (col.name == "Player") {
			active_GUI = false;
		}
	}

	void OnGUI() {
		if (active_GUI) {
			Debug.Log ("GUI is Active");
			GUI.Box (new Rect(Screen.width * .3f, Screen.height * .9f, Screen.width *.4f, 40f), "GUI is active, display content");
		}
	}
}
