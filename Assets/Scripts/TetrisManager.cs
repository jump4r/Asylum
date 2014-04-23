using UnityEngine;
using System.Collections;

public class TetrisManager : MonoBehaviour {

	GameObject player;
	public bool puzzleActive = false;
	TetrisTile lastTile = null;
	TetrisTile[] all_tiles;
	TetrisTile endTile = null;
	TetrisTile startTile = null;

	/* init */
	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		all_tiles = GameObject.FindObjectsOfType<TetrisTile>();

		/* find our start & end tiles */
		foreach (TetrisTile a_tile in all_tiles)
		{
			if(a_tile.name == "tile_end")
				endTile = a_tile;
			else if(a_tile.name == "tile_start")
				startTile = a_tile;
		}

		if(puzzleActive)
			resetPuzzle ();
	}

	void startPuzzle()
	{
		puzzleActive = true;
		resetPuzzle ();
	}

	/* start puzzle */
	void resetPuzzle()
	{
		resetPlayer ();
		lastTile = getCurrentTile ();
	}
	
	/* reset player location ontop of the start tile */
	void resetPlayer()
	{
		player.transform.position = new Vector3 (startTile.transform.position.x, startTile.transform.position.y + 2f, startTile.transform.position.z); 
	}

	/* gets the tile id for the tile you're standing on */
	TetrisTile getCurrentTile()
	{
		RaycastHit hit = new RaycastHit ();

		if (Physics.Raycast (player.transform.position, Vector3.down, out hit, 4.0f))
		{
			//print("Player is on " + hit.transform.parent.parent.name);
			return hit.transform.parent.parent.GetComponent<TetrisTile>();
		}

		return null;
	}
	
	/* update puzzle state*/
	void Update ()
	{
		if (!puzzleActive)
			return;

		/* if the player falls off the puzzle, reset him */
		if (player.transform.position.y < startTile.transform.position.y - 20)
			resetPuzzle ();

		TetrisTile currentTile = getCurrentTile();
		if (currentTile && currentTile != lastTile)
		{

			foreach(TetrisTile a_tile in all_tiles)
			{
				a_tile.startRotation();
			}

			lastTile.freezeTile();
			currentTile.freezeTile();
			lastTile = currentTile;
		}

	}
}
