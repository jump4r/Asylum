using UnityEngine;
using System.Collections;

public class DoorRotate : MonoBehaviour {
	private float smooth = 2.0f;
	private float DoorOpenAngle = -95.0f;
	private float DoorCloseAngle = 0.0f;
	private bool open = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (open) {
			// Lerp towards the target location.
			Quaternion targetRotation = Quaternion.Euler(0, DoorOpenAngle, 0);
			transform.localRotation = Quaternion.Slerp (transform.localRotation, targetRotation, Time.deltaTime * smooth);
		}

		if (!open) {
			Quaternion targetRotation0 = Quaternion.Euler(0, DoorCloseAngle, 0);
			transform.localRotation = Quaternion.Slerp (transform.localRotation, targetRotation0, Time.deltaTime * smooth);
		}
	}

	void OnTriggerEnter(Collider col) {
		open = true;
	}

	void OnTriggerExit(Collider col) {
		open = false;
	}
}
