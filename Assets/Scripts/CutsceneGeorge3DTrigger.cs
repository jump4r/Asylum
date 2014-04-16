using UnityEngine;
using System.Collections;

public class CutsceneGeorge3DTrigger : MonoBehaviour {

	CutsceneGeorge cg;

	// Use this for initialization
	void Start () {
		cg = GameObject.Find ("George Cutscene").GetComponent<CutsceneGeorge> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col) {
		cg.ReposAudio ();
	}
}
