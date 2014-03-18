using UnityEngine;
using System.Collections;

public class DoorSlide : MonoBehaviour {
	private float smooth = 3.0f;
	private float doorSlideDist = 3.0f;
	private bool open = false;
	private bool lerping = true;
	private Vector3 initPos;
	
	// Use this for initialization
	void Start () {
		initPos = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		if (open) {
			// Lerp towards the target location.
			Vector3 t1 = new Vector3(initPos.x + doorSlideDist, initPos.y, initPos.z); // Max Slide Dist
			Vector3 t2 = new Vector3(transform.localPosition.x + Time.deltaTime * smooth, transform.localPosition.y, transform.localPosition.z);
			float min = Mathf.Min (t1.x, t2.x);
			transform.localPosition = new Vector3(min, transform.localPosition.y, transform.localPosition.z);
		}
		
		if (!open) {
			Vector3 t1 = new Vector3(initPos.x, initPos.y, initPos.z);
			Vector3 t2 = new Vector3(transform.localPosition.x - Time.deltaTime * smooth, transform.localPosition.y, transform.localPosition.z);
			float max = Mathf.Max (t1.x, t2.x);
			transform.localPosition = new Vector3(max, transform.localPosition.y, transform.localPosition.z);
		}
	}
	
	void OnTriggerEnter(Collider col) {
		open = true;
	}
	
	IEnumerator OnTriggerExit(Collider col) {
		yield return new WaitForSeconds (2); 
		open = false;
	}
}
