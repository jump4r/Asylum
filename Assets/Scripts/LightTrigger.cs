using UnityEngine;
using System.Collections;

public class LightTrigger : MonoBehaviour {


	void OnTriggerEnter(Collider other)
	{
		/* if the player entered our light trigger teleport them */
		if (other.tag == "Player") 
		{
			if(gameObject.tag == "LightEndTrigger")
			{
				/* exit puzzle goes here, I have no idea how scene switchingworks*/
				//Debug.Log ("You win!");
				Application.LoadLevel(1);
			}
			else
			{
				/* no time hacky dealwithit teleport inside our area */
				other.transform.position = new Vector3(Random.Range (-120f, 120f), 2f, Random.Range (-120f, 120f));
			}
		}
	}
}
