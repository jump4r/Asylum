using UnityEngine;
using System.Collections;

public class DoorRotate : MonoBehaviour {
	private float smooth = 2.0f;
	private float DoorOpenAngle = -135.0f;
	private float DoorCloseAngle = 0.0f;
	private bool open = false; 
	private float yrotate;
	private Vector3 newrotate = new Vector3(0,0,0);
	// Use this for initialization
	void Start () {
		yrotate = transform.parent.transform.rotation.y;
	}
	
	// Update is called once per frame
	void Update () {
		 /* if (open) {
			// Lerp towards the target location.
			Quaternion targetRotation = Quaternion.Euler(0, DoorOpenAngle, 0);
			transform.localRotation = Quaternion.Slerp (transform.localRotation, targetRotation, Time.deltaTime * smooth);
		}
	
		if (!open) {
			Quaternion targetRotation0 = Quaternion.Euler(0, DoorCloseAngle, 0);
			transform.localRotation = Quaternion.Slerp (transform.localRotation, targetRotation0, Time.deltaTime * smooth);
		}*/ 


	}

	void OnTriggerEnter(Collider col) {
		Debug.Log (animation.name);

		// animation ["door_open"].wrapMode = WrapMode.Once;
		if (!open) {
			while(yrotate < yrotate + 90) {//while not rotated
				newrotate.y = transform.parent.transform.position.y + 4;
				transform.parent.transform.position = newrotate;
			}//rotate a lil bit
			open= true;//if rotated 90
				//open = true

		}
	}

	 /* void OnTriggerExit(Collider col) {
		Debug.Log ("Rewind animation");
		animation ["door_open"].speed = -1f;
		animation ["door_open"].time = animation ["door_open"].length;
		animation.Play ("door_open");
	} */
	
}
