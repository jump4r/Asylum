using UnityEngine;
using System.Collections;

public class EndingScript : MonoBehaviour {
	private GameObject player, spawnLoc, renataEnding;
	private bool endingActive, hang;
	private float sceneTime, hangTime;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		endingActive = false;
		hang = false;
		sceneTime = 0f;
		hangTime = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		// Testing/JumpTo Ending
		if (Input.GetKeyDown (KeyCode.K)) {
			Debug.Log ("Loading End Scene");
			AutoFade.LoadLevel(4, 3, 1, Color.black);
		}

		if (endingActive) {
			player.transform.Translate (player.transform.forward / 20);
			renataEnding.transform.Translate (renataEnding.transform.forward/20);
			sceneTime += Time.deltaTime;

			if (sceneTime > 4.60f) {
				//player.GetComponent<CharacterController>().enabled = true;
				//player.transform.position = new Vector3(0,1,10);
				hang = true;
				endingActive = false;
				sceneTime = 0f;

			}
		}

		// hang before level change.
		if (hang) {
			hangTime += Time.deltaTime;
			if (hangTime > 2f) {
				hang = false;
				AutoFade.LoadLevel (1, 3, 1, Color.black);
			}
		}
	}

	void PlayEnding () {
		spawnLoc = GameObject.FindGameObjectWithTag ("EndingSpawn");
		renataEnding = GameObject.FindGameObjectWithTag ("EndingRenata");

		player.transform.position = spawnLoc.transform.position;
		player.transform.rotation = spawnLoc.transform.rotation;

		player.GetComponent<CharacterController> ().enabled = false;
		endingActive = true;
	}

	void OnLevelWasLoaded(int level) {
		if (level == 4) {
			PlayEnding ();
		}
	}
}
