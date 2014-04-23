using UnityEngine;
using System.Collections;

public class PuzzleTile : MonoBehaviour 
{
	public Texture endTile;
	public Texture badTile;
	public bool puzzleStarted = false;

	void OnTriggerEnter(Collider collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			if (transform.parent.tag == "BadTile")
			{
		//		StartCoroutine(ResetLevel());
				//GameObject.FindGameObjectWithTag ("Player").GetComponent<CharacterMotor> ().enabled = false;
				//GameObject.FindGameObjectWithTag ("GameController").GetComponent<MemoryPuzzle> ().isRunning = false;
				Application.LoadLevel(1);
			}
			else if (transform.parent.tag == "GoalTile")
			{
				Application.LoadLevel(0);
				//What to do when you win goes here.
			}
		}
	}

//	IEnumerator ResetLevel()
//	{
		//play fail sound
//	}
}
