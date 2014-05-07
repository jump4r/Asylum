using UnityEngine;
using System.Collections;

public class Footprints : MonoBehaviour {

	bool soundplaying = false;
	CharacterController playerscript;
	// Use this for initialization
	void Start () {

		playerscript = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (playerscript.velocity.magnitude > 1 && playerscript.isGrounded == true && soundplaying == false) {
			audio.Play ();
			soundplaying = true;

		} 
		else if(playerscript.velocity.magnitude < 1 || playerscript.isGrounded == false) {
			audio.Stop();
			soundplaying = false;
		}

	}


}
