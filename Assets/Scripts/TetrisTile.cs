using UnityEngine;
using System.Collections;

public class TetrisTile : MonoBehaviour {

	public bool rotating = false;
	public bool rotated = true;
	private float rotateSpeed = 200.0f;
	private float rotateProgress = 0.0f;
	private const float toRotate = 90.0f;
	private TetrisTrigger trigger = null;

	Transform tile;
	float spinDirection = 1;

	void Awake()
	{
		trigger = gameObject.GetComponentInChildren<TetrisTrigger> ();

		/* find our child tilepieces object */
		foreach (Transform child in transform)
		{
			if(child.tag == "TilePieces")
			{
				tile = child;
				//startRotation();
				break;
			}
		}
	}

	/* this will stop a tile from rotating */
	public void freezeTile()
	{
		rotating = false;
		rotated = true;
	}

	/* call this to start rotation */
	public void startRotation()
	{
		rotating = trigger.shouldRotate;
		spinDirection = (Random.Range (0, 2) == 0) ? 1 : -1;
	}
	
	// Update is called once per frame
	void Update ()
	{

		if(rotating)
		{
			float delta = rotateSpeed * Time.deltaTime * spinDirection;

			if(Mathf.Abs(delta + rotateProgress) >= toRotate)
			{
				tile.transform.Rotate(new Vector3(0, (toRotate - Mathf.Abs (rotateProgress))*spinDirection, 0));
				rotating = false;
				rotated = true;
				rotateProgress = 0f;
				return;
			}
			else
			{
				rotateProgress += delta;
				tile.transform.Rotate(new Vector3(0, delta, 0));
			}
		}
	}
}
