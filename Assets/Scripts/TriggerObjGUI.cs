using UnityEngine;
using System.Collections;

public class TriggerObjGUI : MonoBehaviour {

	private bool active_GUI = false;
	//private ObjectDescriptor child_script = GetComponentInChildren<ObjectDescriptor>();

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col) {
		Debug.Log (col.name);
		if (col.name == "First Person Controller") {
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
			ObjectDescriptor child_script = GetComponentInChildren<ObjectDescriptor>(); // Get the 
			string desc = child_script.desc.ToString();

			Debug.Log (desc);
			GUI.Box (new Rect(Screen.width * .3f, Screen.height * .9f, Screen.width *.4f, 40f), desc);
		}
	}
}
