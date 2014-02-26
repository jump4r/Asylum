using UnityEngine;
using System.Collections;

public class RoomScripts : MonoBehaviour {

	RoomTrigger deep;
	RoomTrigger within;
	GameObject door;
	bool doorOpen = true;

	void Awake()
	{

		/* locate our different zones for this room */
		RoomTrigger[] temp = GetComponentsInChildren<RoomTrigger>();
		foreach(RoomTrigger zone in temp)
		{
			if(zone.name == "deep")
				deep = zone;
			else if (zone.name == "within")
				within = zone;
		}

		//TODO: change this to search only the local object...
		door = GameObject.Find ("Door");
		door.SetActive(false);
	}
	
	/* update the state of the room */
	void Update () {

		/* if the player is deep within the room, do actions here */
		if(deep.playerWithin && within.playerWithin)
		{

			if(doorOpen)
			{
				Debug.Log ("DOOR SLAM");
				door.SetActive(true);
				doorOpen = false;
			}

			/* do more scary stuff */
		}

		/* if the player is in the room, but nearish the door, do stuff here */
		else if(within.playerWithin && !deep.playerWithin) 
		{
			/* transition in / out of some light scary stuff */

		}
	}

}
