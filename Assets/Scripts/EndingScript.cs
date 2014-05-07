using UnityEngine;
using System.Collections;

public class EndingScript : MonoBehaviour {
	private GameObject player, spawnLoc, renataEnding;
	private bool endingActive;
	private float sceneTime;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		endingActive = false;
		sceneTime = 0f;
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

			if (sceneTime > 4.5f) {
				AutoFade.LoadLevel (1, 3, 1, Color.black);
				//player.GetComponent<CharacterController>().enabled = true;
				//player.transform.position = new Vector3(0,1,10);

				endingActive = false;
				sceneTime = 0f;

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
