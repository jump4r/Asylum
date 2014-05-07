using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {


	public bool ovr_present = false;
	private bool wasLocked = false;
	private bool paused;
	private MenuScript menu;
	public int gridX = 7;
	public int gridY = 7;
	public int difficultyLevel = 5;
	private Vector3 mainLevelLoc;
	private GameObject player;

	/* init stuff */
	void Awake ()
	{
		DontDestroyOnLoad(transform.gameObject);
		GameObject mainCamera = GameObject.Find ("Main Camera");
		GameObject riftCamera = GameObject.FindGameObjectWithTag("OVRCamera");
		player = GameObject.FindGameObjectWithTag ("Player");
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
		mainLevelLoc = new Vector3 (0, 2, 10);
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

		if (Input.GetKeyDown (KeyCode.KeypadEnter))
			menu.AcceptSelection();

		if (Input.GetKeyDown (KeyCode.UpArrow))
			menu.ChangeSelectedUp ();



	}

	void OnLevelWasLoaded(int level)
	{
		if (level == 1)
		{
			Debug.Log ("Level 1 loaded.");
			player.transform.position = new Vector3 (0, 2, 10);
			Debug.Log (player.transform.position);
		}
	}

	/* print FPS in upper left */
	void OnGUI()
	{
		GUI.Label(new Rect(0, 0, 100, 100), ((int)(1.0f / Time.smoothDeltaTime)).ToString());     
	}
}
