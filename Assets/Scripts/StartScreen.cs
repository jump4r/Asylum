using UnityEngine;
using System.Collections;

public class StartScreen : MonoBehaviour {

	public GameObject player, door, direction;
	private CharacterController movement;
	private AudioSource sound;
	private TextMesh start, quit;
	private bool starting = false;
	private int selected = 0;
	private float startTime;
	private Quaternion openDoor, closeDoor;
	public enum Axis { LeftXAxis, LeftYAxis, RightXAxis, RightYAxis, LeftTrigger, RightTrigger };
	public enum Button { A, B, X, Y, Up, Down, Left, Right, Start, Back, LStick, RStick, L1, R1 };

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		start = GameObject.Find ("Start").GetComponent<TextMesh> ();
		quit = GameObject.Find ("Quit").GetComponent<TextMesh> ();
		door = GameObject.Find ("Door");
		direction = GameObject.Find ("Direction");
		sound = gameObject.GetComponent<AudioSource> ();
		movement = player.GetComponent<CharacterController> ();
		movement.enabled = false;
		closeDoor = door.transform.rotation;
		door.transform.Rotate (0, 90, 0);
		openDoor = door.transform.rotation;
		door.transform.Rotate (0, -90, 0);
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(OVRGamepadController.GPC_GetButton ((int)Button.A))
		{
			if (selected == 0)
			{
				StartSequence ();
			}
			else if (selected == 1)
			{
				Application.Quit();
			}
		}

		if (OVRGamepadController.GPC_GetAxis((int)(Axis.LeftXAxis)) > 0)
		{
			selected = 1;
		}
		else if (OVRGamepadController.GPC_GetAxis((int)(Axis.LeftXAxis)) < 0)
		{
			selected = 0;
		}

		if (selected == 0)
		{
			start.renderer.material.SetColor("_Color", Color.white);
			quit.renderer.material.SetColor("_Color", Color.gray);
		}
		else if (selected == 1)
		{
			start.renderer.material.SetColor("_Color", Color.gray);
			quit.renderer.material.SetColor("_Color", Color.white);
		}

		if(starting)
		{
			door.transform.Rotate (0,1,0);
			player.transform.Translate(transform.forward / 8);
			if (Time.time - startTime > 3.0f)
			{
				Application.LoadLevel(1);
			}
		}
	}

	void StartSequence()
	{
		starting = true;
		startTime = Time.time;
		sound.Play ();
		//player.transform.position = player.transform.position + transform.forward;
	}
}
