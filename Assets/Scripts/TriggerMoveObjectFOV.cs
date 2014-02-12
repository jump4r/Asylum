using UnityEngine;
using System.Collections;

public class TriggerMoveObjectFOV : MonoBehaviour {

	public Vector3 pos1 = new Vector3(5, 0, 5);
	private Plane[] planes;
	private Camera cam;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	void OnTriggerEnter(Collider col) {
		Debug.Log ("Triggered");
		cam = Camera.main;
		planes = GeometryUtility.CalculateFrustumPlanes (cam); // Calculate the planes of the camera, Determine if obj is in those planes.
		if (col.name == "Player" && !(GeometryUtility.TestPlanesAABB(planes, GameObject.Find ("SecondCube").collider.bounds))) {
			Debug.Log ("Success on FOV Move!");
			Vector3 cur_pos = GameObject.Find ("SecondCube").transform.position; // Find the GameObject
			GameObject.Find ("SecondCube").transform.position = new Vector3(cur_pos.x, cur_pos.y + 5, cur_pos.z);
		}
	}
}
