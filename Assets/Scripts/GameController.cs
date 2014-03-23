using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {


	public bool ovr_present = false;
	private bool wasLocked = false;


	/* init stuff */
	void Awake ()
	{
		GameObject mainCamera = GameObject.Find ("Main Camera");
		GameObject riftCamera = GameObject.FindGameObjectWithTag("OVRCamera");
		GameObject player = GameObject.FindGameObjectWithTag ("Player");

		if(OVRDevice.SensorCount > 0) {
			mainCamera.SetActive(false);
			player.GetComponent<MouseLook>().enabled = false;

			riftCamera.SetActive(true);
			ovr_present = true;
			Debug.Log ("Rift found!");
		} else {
			mainCamera.SetActive(true);
			riftCamera.SetActive(false);
			Debug.Log ("Rift not found!");
		}

		Screen.lockCursor = true;
	}

	/* update */
	void Update () {

		/* unlock cursor &| exit */
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown (KeyCode.J))
		{
			Application.Quit();
			Screen.lockCursor = false;
		}
	}

	/* print FPS in upper left */
	void OnGUI()
	{
		GUI.Label(new Rect(0, 0, 100, 100), ((int)(1.0f / Time.smoothDeltaTime)).ToString());     
	}
}
