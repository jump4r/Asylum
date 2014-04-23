using UnityEngine;
using System.Collections;

public class TetrisTrigger : MonoBehaviour {

	public bool shouldRotate = false;


	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			shouldRotate = true;
			//print (transform.parent.name + " should roate!");
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			shouldRotate = false;
			//print (transform.parent.name + " should not rotate now!");
		}
	}
}
