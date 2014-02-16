using UnityEngine;
using System.Collections;

public class ObjActions : MonoBehaviour {

	public bool isObserved = true;
	private bool hasChanged = false;
	private Vector3 normalScale;

	/* initialize or save anything we need here */
	void Awake()
	{
		normalScale = transform.localScale;
	}

	/* randomly change the scale of the object within reason */
	void changeScale()
	{
		if(normalScale != transform.localScale)
			transform.localScale = normalScale;

		transform.localScale += (new Vector3(Random.Range (.2f, 2f), Random.Range (.2f, 2f), Random.Range (.2f, 2f)));
		//print (transform.localScale);
	}

	
	void Update ()
	{

		/* if the player is watching the object*/
		if(isObserved)
		{
			//print (transform.name + " is being observed");
			hasChanged = false;
			return;
		}

		/* otherwise do silly stuff when nobody is looking */
		if(!hasChanged)
		{
			changeScale();

			/* add more silly stuff here */

			/* make sure we tell the object not do more silly stuff next time */
			hasChanged = true;
		}
	}
}
