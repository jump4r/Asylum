using UnityEngine;
using System.Collections;

public class CutsceneScripts : MonoBehaviour {

	GameController GC;
	GameObject[] players;
	GameObject fps_controller;
	GameObject ovr_controller;
	// Use this for initialization
	void Awake ()
	{
		GC = GameObject.Find("GameVariables").GetComponent<GameController>();
		players = GameObject.FindGameObjectsWithTag ("Player");

		//assign controllers 
		foreach (GameObject controller in players)
		{
			if(controller.name == "OVRPlayerController")
				ovr_controller = controller;
			else if(controller.name == "First Person Controller")
				fps_controller = controller;
		}
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (fps_controller.name);
	}
}
