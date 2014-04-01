using UnityEngine;
using System.Collections;

public class DoorRotate : MonoBehaviour {
	private float smooth = 2.0f;
	private float DoorOpenAngle = -135.0f;
	private float DoorCloseAngle = 0.0f;
	private bool open = false; 

	// Use this for initialization
	void Start () {

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

		if (!animation.isPlaying) {
			audio.Stop();
		}
	}

	void OnTriggerEnter(Collider col) {
		Debug.Log (animation.name);

		// animation ["door_open"].wrapMode = WrapMode.Once;
		if (!open) {
			animation ["door_open"].speed = .5f + Random.value;
			open = true;
			animation.Play ("door_open");
			audio.Play ();
		}
	}

	 /* void OnTriggerExit(Collider col) {
		Debug.Log ("Rewind animation");
		animation ["door_open"].speed = -1f;
		animation ["door_open"].time = animation ["door_open"].length;
		animation.Play ("door_open");
	} */
	
}
