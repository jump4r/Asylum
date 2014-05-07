using UnityEngine;
using System.Collections;

public class OrigionalPlatform : MonoBehaviour {

	bool winCondition = false;
	bool timetodrop = false;
	public float speed = .2f;
	Vector3 downdest = new Vector3(0,0,0);
	float origionaltime = 0.0f;
	public GameObject IAMLITERALLYFUNCTIONALLYRETARDED;
	Vector3 death = new Vector3(0,0,0);

	// Use this for initialization
	void Start () {
		var otherpos = transform.position;
		downdest.x = otherpos.x;
		downdest.z = otherpos.z;
		death.y = otherpos.y;
		IAMLITERALLYFUNCTIONALLYRETARDED = GameObject.FindGameObjectWithTag("Player");
		IAMLITERALLYFUNCTIONALLYRETARDED.transform.position = new Vector3 (900, 1101, 538);

		/*for (int i = 0; i < IAMLITERALLYFUNCTIONALLYRETARDED.Length; i++)
		{
			if (IAMLITERALLYFUNCTIONALLYRETARDED[i].transform.position.y < 640) Destroy (IAMLITERALLYFUNCTIONALLYRETARDED[i]);
		}*/
	

	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time - origionaltime > 2)
			timetodrop = true;
		if(timetodrop)
			transform.position = Vector3.MoveTowards(transform.position, downdest, speed); //goes straight down
		if(transform.position.y <= 930)
			speed = .1f;
		death.y = IAMLITERALLYFUNCTIONALLYRETARDED.transform.position.y;

		if(death.y <= 630 && winCondition == false) {
			Application.LoadLevel(1);
			Debug.Log ("LOADLEVEL");
			winCondition = true;
			/* if (GameObject.FindGameObjectWithTag("Journal2").GetComponent<Journal>().GetCurrentJournal() > 8) {
				Application.LoadLevel (5);
			}
			else {
				Application.LoadLevel("thescene");
				Debug.Log ("LOADLEVEL");
			}*/
		}
	}
}
