using UnityEngine;
using System.Collections;

public class MemoryPuzzle : MonoBehaviour
{
	public Material badTile;
	public Material goodTile;
	public Material blankTile;	
	public Material endTile;
	public GameObject glowingTile;
	public int gridX = 7;
	public int gridY = 7;
	private float spacing = 5f;
	private int startX;
	private int startY;
	private int direction;
	private bool pathFound;
	public int difficultyLevel = 5;
	int[,] grid;
	
	void Start()
	{
		grid = new int [gridX,gridY];
		FindPath ();
		for (int y = 0; y < gridY; y++)
		{
			for (int x = 0; x < gridX; x++)
			{
				Vector3 pos = new Vector3(x, 0.01f, y) * spacing;

				if (grid[x,y] == 1)
				{
					glowingTile.renderer.material = goodTile;
				}
				else if (grid[x,y] == 2)
				{
					glowingTile.renderer.material = endTile;
				}
				else
				{
					glowingTile.renderer.material = badTile;
				}
				Instantiate(glowingTile, pos, Quaternion.identity);
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

		for (int i = 0; i < difficultyLevel; i++)
		{
			Debug.Log("Finding path");
			pathFound = false;
			direction = Random.Range (0,4);
			int j = 0;
			while (pathFound == false && j < 4)
			{
				Debug.Log("Finding path");
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