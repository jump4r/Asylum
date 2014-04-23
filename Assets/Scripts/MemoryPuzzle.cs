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

	private GameObject startTile, player, gameController;
	private int startX, startY, direction, firstX, firstY, gridX, gridY, difficultyLevel;
	private bool pathFound;
	private GameObject [,] gridTiles;
	private int[,] grid;
	private float startTime, speed;
	private Vector3 startPos, endPos;
	private float spacing = 5f;
	private bool puzzleStarted = false;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		gameController = GameObject.FindGameObjectWithTag ("GameController");
		gridX = gameController.GetComponent<GameController> ().gridX;
		gridY = gameController.GetComponent<GameController> ().gridY;
		difficultyLevel = gameController.GetComponent<GameController> ().difficultyLevel;
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
		StartingSequence ();
		CheckPlayerHeight ();
	}

	void StartingSequence ()
	{
		if (startTile.transform.position.y >= 0)
		{
			speed = (Time.time - startTime) / (difficultyLevel / 2.0f);
			startTile.transform.position = new Vector3(Mathf.SmoothStep(startPos.x, endPos.x, speed), Mathf.SmoothStep(startPos.y, endPos.y, speed/2), Mathf.SmoothStep(startPos.z, endPos.z, speed));
			player.transform.position = startTile.transform.position + transform.up;
		}
		else if (!puzzleStarted)
		{
			puzzleStarted = true;
			for (int y = 0; y < gridY; y++)
			{
				for (int x = 0; x < gridX; x++)
				{
					gridTiles[x,y].renderer.material = blankTile;
				}
			}
		}
	}

	void CheckPlayerHeight()
	{
		if(player.transform.position.y < -10)
		{
			player.transform.position = endPos + transform.up * 10;
		}
	}

	void MakeStartPlatform()
	{
		float xStart = gridX / 2.0f * spacing - spacing * 0.5f;
		float yStart = spacing * -2;
		float mag = Mathf.Sqrt(Mathf.Pow ((firstX - xStart), 2) + Mathf.Pow ((firstY - yStart), 2));
		startPos = new Vector3(xStart, mag * 2, yStart);
		startTile = Instantiate(glowingTile, startPos, Quaternion.identity) as GameObject;
		startTile.tag = "GoodTile";
		startTile.renderer.material = invisTile;
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
					gridTiles [x,y] = Instantiate(glowingTile, pos, Quaternion.identity) as GameObject;
					gridTiles [x,y].renderer.material = goodTile;
					gridTiles [x,y].tag = "GoodTile";
					gridTiles [x,y].name = x.ToString() + y.ToString();
				}
				else if (grid[x,y] == 2)
				{
					gridTiles [x,y] = Instantiate(glowingTile, pos, Quaternion.identity) as GameObject;
					gridTiles [x,y].renderer.material = endTile;
					gridTiles [x,y].tag = "GoalTile";
					gridTiles [x,y].name = x.ToString() + y.ToString();
				}
				else
				{
					gridTiles [x,y] = Instantiate(glowingTile, pos, Quaternion.identity) as GameObject;
					gridTiles [x,y].renderer.material = badTile;
					gridTiles [x,y].tag = "BadTile";
					gridTiles [x,y].name = x.ToString() + y.ToString();
				}
				//gridTiles [x,y] = Instantiate(glowingTile, pos, Quaternion.identity) as GameObject;
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