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
			if (transform.parent.tag == "GoodTile")
			{
				GameObject.FindGameObjectWithTag("GameController").GetComponent<MemoryPuzzle>().puzzleStarted = true;
			}
			if (transform.parent.tag == "BadTile")
			{
				StartCoroutine(ResetLevel());
			}
			else if (transform.parent.tag == "GoalTile")
			{
				//What to do when you win goes here.
			}
		}
	}

	IEnumerator ResetLevel()
	{
		//play fail sound
		GameObject.FindGameObjectWithTag ("Player").GetComponent<CharacterMotor> ().enabled = false;
		yield return new WaitForSeconds(3);
		Application.LoadLevel(Application.loadedLevel);
	}
}
