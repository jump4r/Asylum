using UnityEngine;
using System.Collections;

public class ObjActions : MonoBehaviour {

	public bool isObserved = true;
	public bool isNearby = false;
	private bool cutsceneOverride = false;
	private bool hasChanged = false;
	private Vector3 normalScale;
	private Vector3 normalPosition;
	private MeshRenderer mesh;
	//private Rigidbody phys;
	private bool visible = true;
	private CutsceneScripts cutsceneScripts;

	/* initialize or save anything we need here */
	void Awake()
	{
		normalScale = transform.localScale;
		normalPosition = transform.localPosition;
		mesh = gameObject.GetComponent<MeshRenderer>();
		cutsceneScripts = GameObject.Find ("GameVariables").GetComponent<CutsceneScripts>();
		//phys = gameObject.GetComponent<Rigidbody>();
	}

	/* randomly change the scale of the object within reason */
	void changeScale()
	{
		if(normalScale != transform.localScale)
		{
			transform.localScale = normalScale;
			transform.localPosition = normalPosition;
		}

		transform.localScale += (new Vector3(Random.Range (.2f, 2f), Random.Range (.2f, 2f), Random.Range (.2f, 2f)));
	}

	/* toggles object visibility */
	void changeVisibility()
	{
		if(cutsceneOverride)
			return;

		visible = !visible;
		mesh.enabled = visible;
		rigidbody.detectCollisions = visible;
		rigidbody.useGravity = visible;
		//Debug.Log("Changing " + this.name + "'s visibility to: " + visible);
	}

	/* changes objects orientation */
	void changeOrientation()
	{
		/* roll, yaw, pitch */
		transform.Rotate(new Vector3(0, Random.Range (0,360), 0));
	}

	void moveObject()
	{

	}

	
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			isNearby = true;
//			Debug.Log("Added " + other.name);
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			isNearby = false;
//			Debug.Log("removed " + other.name);
		}
	}
	
	void Update ()
	{
		cutsceneOverride = isNearby && cutsceneScripts.cs_active;

		/* if the player is watching the object*/
		if(isObserved && !cutsceneOverride)
		{
			//print (transform.name + " is being observed");
			hasChanged = false;
			return;
		}

		/* otherwise do silly stuff when nobody is looking */
		if(!hasChanged || cutsceneOverride)
		{
			/* give it a chance to become visible */
			if(!visible & (Random.Range (0,2) == 1))
				changeVisibility();

			/* experience some effect */
			else
			{

				int a_trip = Random.Range (0,3);
				//Debug.Log ("Experience effect " + a_trip);
				switch(a_trip)
				{
					case 0:
						changeScale(); break;
					case 1:
						changeOrientation(); break;
					case 2:
						changeVisibility(); break;

				}
			}

			/* make sure we tell the object not do more silly stuff next time */
			hasChanged = true;
		}
	}
}
