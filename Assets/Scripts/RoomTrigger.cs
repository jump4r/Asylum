using UnityEngine;
using System.Collections;

public class RoomTrigger : MonoBehaviour {

	public bool playerWithin = false;
	BoxCollider zone;

	void Awake()
	{
		zone = transform.GetComponent<BoxCollider>();
	}

	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			playerWithin = true;
			Debug.Log ("Player entered trigger!");
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.tag == "Player")
		{
			playerWithin = false;
			Debug.Log ("Player left trigger!");
		}
	}


}
