using UnityEngine;
using System.Collections;

public class LightPuzzleScript : MonoBehaviour {

	private GameObject Greenlight;
	private GameObject[] Redlights;
	private float timestep = 0.3f;
	private float timer = 0f;
	private float green_randomize = 3f;
	private bool green_placed = false;

	// Use this for initialization
	void Awake ()
	{
		Greenlight = GameObject.FindGameObjectWithTag ("Greenlight");
		Redlights = GameObject.FindGameObjectsWithTag ("Redlight");
		timer = timestep;

		/* hardcode dat start position because last minute puzzles :D */
		GameObject.FindGameObjectWithTag ("Player").transform.position = new Vector3 (0f, 2f, -124.4244f);
	}

	/* returns a random light location within our puzzle bounds */
	private Vector3 randomLocation()
	{
		return new Vector3(Random.Range (-120f, 120f), 13f, Random.Range (-120f, 120f));
	}

	/* make a random green location away from the player */
	private Vector3 randomGreenLocation()
	{
		return new Vector3(Random.Range (-120f, 120f), 13f, Random.Range (25f, 120f));
	}

	/* returns random location for new red light */
	private Vector3 randomRedLocation ()
	{
		Vector3 rand = Vector3.zero; bool reroll = true;
		while(reroll)
      	{
			reroll = false;
			rand = randomLocation();

			/* no overlapping red lights */
			for(int i = 0; i < Redlights.Length; i++){
				if(Vector3.Distance(rand,Redlights[i].transform.localPosition) < 20f)
				{
					reroll = true;
					break;
				}
			}

			/* doesn't overlapp with green light */
			if(Vector3.Distance(rand,Greenlight.transform.localPosition) < 20f)
				reroll = true;
		}

		return rand;
	}

	/* move lights / update puzzle */
	void Update ()
	{
		timer -= Time.deltaTime;

		/* move a red light */
		if (timer <= 0f)
		{
			timer = timestep;
			Redlights[Random.Range(0, Redlights.Length)].transform.localPosition = randomRedLocation();
		}

		/* place the green light in some random location */
		if (!green_placed) 
		{
			green_randomize -= Time.deltaTime;
			if(green_randomize < 0f)
			{
				Greenlight.transform.localPosition = randomGreenLocation ();
				green_placed = true;
			}
		}
	}
}
