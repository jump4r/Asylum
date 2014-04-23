using UnityEngine;
using System.Collections;

public class PuzzleTile : MonoBehaviour 
{
	public Texture blankTile;

	void OnTriggerEnter(Collider collision)
	{
		if (gameObject.transform.parent.renderer.material.mainTexture == blankTile)
		{
			if (collision.gameObject.tag == "Player")
			{
				if (transform.parent.tag == "BadTile")
				{
					Application.LoadLevel(1);
				}
				else if (transform.parent.tag == "GoalTile")
				{
					Application.LoadLevel(0);
				}
			}
		}
	}
}
