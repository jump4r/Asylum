using UnityEngine;
using System.Collections;

public class TetrisTile : MonoBehaviour {

	public bool rotating = false;
	public bool rotated = true;
	private float rotateSpeed = 1.0f;
	private float rotateProgress = 0.0f;
	private const float toRotate = 90.0f;
	Transform tile;

	void Awake()
	{
		tile = transform.FindChild("tile_pieces");
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(rotating)
		{
			rotateProgress += rotateSpeed * Time.deltaTime;
			tile.transform.Rotate(new Vector3(0, rotateSpeed*Time.deltaTime, 0));

			if(rotateProgress >= toRotate)
			{
				rotating = false;
				rotated = true;
			}
		}
	}
}
