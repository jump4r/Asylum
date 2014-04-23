using UnityEngine;
using System.Collections;

public class MemoryPuzzle : MonoBehaviour
{
	public Material badTile;
	public Material goodTile;
	public Material blankTile;	
	public Material endTile;
	public Material invisTile;
	public GameObject glowingTile;
	public int gridX = 7;
	public int gridY = 7;
	public int difficultyLevel = 5;
	public bool puzzleStarted = false;

	private GameObject startTile, player;
	private float spacing = 5f;
	private int startX, startY, direction;
	private bool pathFound;
	private GameObject [,] gridTiles;
	private int[,] grid;
	private float startTime;
	private int firstX, firstY;
	private Vector3 startPos, endPos;
	private float speed = 0;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		grid = new int [gridX,gridY];
		gridTiles = new GameObject[gridX, gridY];
	}

	void Start()
	{
		FindPath ();
		MakeGrid ();
		MakeStartPlatform ();
	}

	void Update()
	{
		CheckForPuzzleStart ();
		MoveStartPlatform ();
		CheckPlayerHeight ();
	}

	void MoveStartPlatform ()
	{
		if (startTile.transform.position.y >= 0)
		{
			speed = (Time.time - startTime) / (difficultyLevel / 2.0f);
			startTile.transform.position = new Vector3(Mathf.SmoothStep(startPos.x, endPos.x, speed), Mathf.SmoothStep(startPos.y, endPos.y, speed/2), Mathf.SmoothStep(startPos.z, endPos.z, speed));
			player.transform.position = startTile.transform.position+transform.up;
		}
	}

	void CheckPlayerHeight()
	{
		if(player.transform.position.y < -10)
		{
			player.transform.position = endPos + transform.up * 10;
		}
	}

	void CheckForPuzzleStart()
	{
		if ((Time.time - startTime > difficultyLevel && puzzleStarted == false) || (Time.time - startTime < difficultyLevel && puzzleStarted == true))
		{
			puzzleStarted = true;
			for (int y = 0; y < gridY; y++)
			{
				for (int x = 0; x < gridX; x++)
				{
					gridTiles[x,y].renderer.material = blankTile;
					gridTiles[x,y].GetComponentInChildren<PuzzleTile>().puzzleStarted = true;
				}
			}
		}
	}

	void MakeStartPlatform()
	{
		float xStart = gridX / 2.0f * spacing - spacing * 0.5f;
		float yStart = spacing * -2;
		float mag = Mathf.Sqrt(Mathf.Pow ((firstX - xStart), 2) + Mathf.Pow ((firstY - yStart), 2));
		startPos = new Vector3(xStart, mag * 2, yStart);
		glowingTile.renderer.material = invisTile;
		glowingTile.tag = "";
		startTile = Instantiate(glowingTile, startPos, Quaternion.identity) as GameObject;
		startTime = Time.time;
		endPos = new Vector3 (firstX, -0.05f, firstY) * spacing;
	}

	void MakeGrid()
	{
		for (int y = 0; y < gridY; y++)
		{
			for (int x = 0; x < gridX; x++)
			{
				Vector3 pos = new Vector3(x, 0, y) * spacing;
				
				if (grid[x,y] == 1)
				{
					glowingTile.renderer.material = goodTile;
					glowingTile.tag = "GoodTile";
				}
				else if (grid[x,y] == 2)
				{
					glowingTile.renderer.material = endTile;
					glowingTile.tag = "GoalTile";
				}
				else
				{
					glowingTile.renderer.material = badTile;
					glowingTile.tag = "BadTile";
				}
				gridTiles [x,y] = Instantiate(glowingTile, pos, Quaternion.identity) as GameObject;
			}
		}
	}
	
	void FindPath()
	{
		if (Random.Range (0, 2) == 1)
		{
			startX = Random.Range (1, gridX - 1);
			if (Random.Range (0, 2) == 1)
			{
				startY = 0;
			}
			else
			{
				startY = gridY - 1;
			}
		}
		else
		{
			startY = Random.Range (1, gridY - 1);
			if (Random.Range (0, 2) == 1)
			{
				startX = 0;
			}
			else
			{
				startX = gridX - 1;
			}
		}
		grid [startX, startY] = 1;
		firstX = startX;
		firstY = startY;

		for (int i = 0; i < difficultyLevel; i++)
		{
			//Debug.Log("Finding path");
			pathFound = false;
			direction = Random.Range (0,4);
			int j = 0;
			while (pathFound == false && j < 4)
			{
				//Debug.Log("Finding path");
				if (direction == 0)
				{
					if (startY < gridY - 2 && startX < gridX-1 && startX > 0 && grid[startX-1,startY+1] == 0 && grid[startX,startY+2] == 0 && grid[startX+1,startY+1] == 0)
					{
						startY++;
						grid[startX,startY] = 1;
						pathFound = true;
					}
					else
					{
						direction++;
						j++;
					}
				}
				else if (direction == 1)
				{
					if (startX < gridX - 2 && startY < gridY-1 && startY > 0 && grid[startX+1,startY+1] == 0 && grid[startX+2,startY] == 0 && grid[startX+1,startY-1] == 0)
					{
						startX++;
						grid[startX,startY] = 1;
						pathFound = true;
					}
					else
					{
						direction++;
						j++;
					}
				}
				else if (direction == 2)
				{
					if (startY > 1 && startX < gridX-1 && startX > 0 && grid[startX-1,startY-1] == 0 && grid[startX,startY-2] == 0 && grid[startX+1,startY-1] == 0)
					{
						startY--;
						grid[startX,startY] = 1;
						pathFound = true;
					}
					else
					{
						direction++;
						j++;
					}
				}
				else if (direction == 3)
				{
					if (startX > 1 && startY < gridY-1 && startY > 0 && grid[startX-1,startY-1] == 0 && grid[startX-2,startY] == 0 && grid[startX-1,startY+1] == 0)
					{
						startX--;
						grid[startX,startY] = 1;
						pathFound = true;
					}
					else
					{
						direction = 0;
						j++;
					}
				}
			}
		}
		grid [startX, startY] = 2;
	}
}