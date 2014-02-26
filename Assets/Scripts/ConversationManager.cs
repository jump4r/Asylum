using UnityEngine;
using System.Collections;

public class ConversationManager : MonoBehaviour {
	Inmate name1; // List of Inmates, for their respective states, importable by the 
	Inmate name2;

	// Use this for initialization
	void Start () {
		name1 = GameObject.FindGameObjectWithTag("Person1").GetComponent<Inmate>();
		name2 = GameObject.Find("Person2Model").GetComponent<Inmate>();
	}
	
	// Update is called once per frame
	void Update () {
		if (name1.GetCurrentConv () != 0) {
			Debug.Log ("I was right!");
		}
		else {
//			Debug.Log (name1.GetCurrentConv());
		}
	}
}
