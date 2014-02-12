using UnityEngine;
using System.Collections;

public class TriggerMoveObject : MonoBehaviour {

	public Vector3 pos1 = new Vector3(9, 2, 20);
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col) {
		Debug.Log (col.name);
		if (col.name == "Player") {
			GameObject.Find ("FirstCube").transform.position = pos1; // Find the GameObject
		}
	}
}
