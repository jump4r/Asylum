using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {


	public bool ovr_present = false;
	private bool wasLocked = false;
	private bool paused;
	private MenuScript menu;


	/* init stuff */
	void Awake ()
	{
		DontDestroyOnLoad(transform.gameObject);
		GameObject mainCamera = GameObject.Find ("Main Camera");
		GameObject riftCamera = GameObject.FindGameObjectWithTag("OVRCamera");
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		menu = GetComponent<MenuScript> ();

		if(OVRDevice.IsSensorPresent()) {
			mainCamera.SetActive(false);
			//player.GetComponent<MouseLook>().enabled = false;

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
			menu.ActivateMenu();
		}

		// changed selected menu item;
		if (Input.GetKeyDown (KeyCode.DownArrow))
			menu.ChangeSelectedDown ();

		if (Input.GetKeyDown (KeyCode.UpArrow))
			menu.ChangeSelectedUp ();
	}

	/* print FPS in upper left */
	void OnGUI()
	{
		GUI.Label(new Rect(0, 0, 100, 100), ((int)(1.0f / Time.smoothDeltaTime)).ToString());     
	}
}
