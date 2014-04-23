using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

	// Use this for initialization
	//public Transform dest;

	Vector3 dest = new Vector3(0, 0, 0);

	public float speed;
	Vector3 origionalheight = new Vector3(0,0,0);

	// Use this for initialization
	void Start () {
		//dest.position = new Vector3(transform.position.x, 0,transform.position.z);
		var pos = transform.position;
		dest.x = pos.x;
		dest.z = pos.z;
		var otherpos = transform.position;
		origionalheight.x = otherpos.x;
		origionalheight.y = otherpos.y;
		origionalheight.z = otherpos.z;

	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.MoveTowards(transform.position, dest, speed);
		if(transform.position.y <= 150)
			transform.position = origionalheight;

	}
}