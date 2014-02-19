using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {


	private bool wasLocked = false;

	/* init stuff */
	void Awake ()
	{
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
