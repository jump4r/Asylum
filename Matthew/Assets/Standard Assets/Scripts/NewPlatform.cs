using UnityEngine;
using System.Collections;

public class NewPlatform : MonoBehaviour {
	public Vector3 updest = new Vector3(907, 1125, 538);
	float origionalY;
	public bool firstplatform = false;
	public float speed = .2f;
	bool goingup = true;
	Vector3 downdest = new Vector3(0,0,0);
	bool nothityet = true;

	public GameObject level;
	GameObject clone;
	NewPlatform newscript;
	float origionaltime = 0.0f;
	Vector3 newplatformpos;

	// Use this for initialization
	void Start () {

		var otherpos = transform.position;
		downdest.x = otherpos.x;
		downdest.z = otherpos.z;


	}
	
	// Update is called once per frame
	void Update () {
		if(firstplatform == false) { //normal platform
			if(goingup) {
				transform.position = Vector3.MoveTowards(transform.position, updest, speed);
				if(transform.position == updest){
					speed = .1f;
					updest.y = 1000;
				}
			}
			else
				transform.position = Vector3.MoveTowards(transform.position, downdest, speed); //goes straight down

		}
		else { //for origional platform

			if(Time.time - origionaltime > 2) //time to act as normal platform
				firstplatform = false;
		}
		if(origionalY - transform.position.y >= 75)
			speed = .1f;
	}

	Vector3 generateplatform() {
		Vector3 platformpos = new Vector3(0,0,0);

		float min = transform.position.z - 63.2f;
		float max = transform.position.z + 63.2f;
		float centerz = transform.position.z;
		float centerx = transform.position.x;
		float randomz = Random.Range (min,max);

		float calculatedx = (Mathf.Sqrt(3994.25f - Mathf.Pow(randomz - centerz,2)) + centerx);
		float postvcalculatedx = Mathf.Abs(calculatedx);

		platformpos.z = randomz;
		platformpos.x = postvcalculatedx;
		platformpos.y = transform.position.y - 100;
		//Debug.Log("1 New x pos is: " + platformpos.x + "New z pos is: " + platformpos.z);
		return platformpos;
	}

	void OnCollisionEnter(Collision hit) {
		if(nothityet){
			Debug.Log ("Hit, going down");
			goingup = false;
			//make new platform

			speed = .2f;
			//newplatformpos = new Vector3(0, 0, 0);
			Quaternion platformRotation = transform.rotation;
			newplatformpos = generateplatform();
			//Debug.Log("2 New x pos is: " + newplatformpos.x + "New z pos is: " + newplatformpos.z);

			clone = Instantiate(level,newplatformpos, platformRotation)as GameObject;
			nothityet = false;
			newscript = clone.GetComponent<NewPlatform>();
			Vector3 newpos = new Vector3(0,0,0); //need to stay 7 away
			var pos = transform.position;
			var clonepos = clone.transform.position;

			float x, z;
			x = (pos.x - clonepos.x)*-1;
			z = (pos.z - clonepos.z)*-1;
			newpos.x = Mathf.Sin (Mathf.Atan2(x,z)) * 7 + pos.x;
			newpos.z = Mathf.Cos (Mathf.Atan2(x,z)) * 7 + pos.z;
			newpos.y = pos.y - 75;

			//Debug.Log (" x: " + z + " y: " + x);
			//Debug.Log (" atan2: " + Mathf.Atan2(x,z)*Mathf.Rad2Deg);
			//Debug.Log (" adding x: " + Mathf.Cos (Mathf.Atan2(x,z))*7 + " adding y: " + (Mathf.Sin (Mathf.Atan2(x,z)) * 7));
			//Debug.Log (" sin is " + Mathf.Sin (Mathf.Atan2(x,z)));

			origionalY= clonepos.y;
			newscript.updest = newpos;

			//newscript.speed -= .02f;




		}

	}
}
