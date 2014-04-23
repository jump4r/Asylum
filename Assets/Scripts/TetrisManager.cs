using UnityEngine;
using System.Collections;

public class TetrisManager : MonoBehaviour {

	private GameObject player;
	private Vector3 player_position;
	private Quaternion player_rotation;
	private GameObject currentPuzzle;
	private TetrisTile[] all_tiles;
	private TetrisTile startTile = null;
	private TetrisTile endTile = null;
	private TetrisTile lastTile = null;

	public bool puzzleActive = false;

	/* init */
	private void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");

		if(puzzleActive)
			loadPuzzle (0);
	}

	
	/* to start the puzzle */
	public void startPuzzle(int puzzle_id)
	{
		player_position = player.transform.position;
		player_rotation = player.transform.rotation;
		loadPuzzle (puzzle_id);
		puzzleActive = true;
	}

	/* loads selected puzzle, places player on puzzle */
	private void loadPuzzle(int puzzle_id)
	{
		currentPuzzle = (GameObject)Instantiate (Resources.Load ("tetris_puzzles/TilePuzzle_"+puzzle_id));
		all_tiles = GameObject.FindObjectsOfType<TetrisTile>();
		
		/* find our start & end tiles */
		foreach (TetrisTile a_tile in all_tiles)
		{
			if(a_tile.name == "tile_end")
				endTile = a_tile;
			else if(a_tile.name == "tile_start")
				startTile = a_tile;
		}

		/* put the player on the puzzle */
		resetPlayer ();
	}

	/* ends puzzle */
	private void destroyPuzzle()
	{
		puzzleActive = false;
		player.transform.position = player_position + Vector3.up*2;
		player.transform.rotation = player_rotation;
		Destroy (currentPuzzle);
	}
	
	/* reset player location ontop of the start tile */
	void resetPlayer()
	{
		player.transform.position = new Vector3 (startTile.transform.position.x, startTile.transform.position.y + 2f, startTile.transform.position.z); 
		lastTile = getCurrentTile ();
	}

	/* gets the tile id for the tile you're standing on */
	TetrisTile getCurrentTile()
	{
		RaycastHit hit = new RaycastHit ();

		/* figure out what platform is beneath the player */
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
			resetPlayer ();

		TetrisTile currentTile = getCurrentTile();

		/* if landed on a new tile ... */
		if (currentTile && currentTile != lastTile)
		{

			/* rotate all tiles */
			foreach(TetrisTile a_tile in all_tiles)
				a_tile.startRotation();

			/* don't rotate the tile we landed on, or the one we jumed from */
			lastTile.freezeTile();
			currentTile.freezeTile();


			/* reached the end of the puzzle, destroy and head back to game */
			if(currentTile.name == "tile_end")
			{
				/* do other shit here if we need to */

				destroyPuzzle();
				return;
			}

			/* save current tile as the new last tile */
			lastTile = currentTile;
		}

	}
}
