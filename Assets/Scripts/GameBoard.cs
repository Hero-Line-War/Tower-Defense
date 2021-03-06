using UnityEngine;
using System.Collections.Generic;
using System;

public class GameBoard : MonoBehaviour {

    [SerializeField]
	GameTile tilePrefab = default;

	Vector2Int size;

	GameTile[] tiles;

	Queue<GameTile> searchFrontier = new Queue<GameTile>();

	List<GameTile> spawnPoints = new List<GameTile>();

	List<GameTileContent> updatingContent = new List<GameTileContent>();

	GameTileContentFactory contentFactory;

	private int turretToBuild;
	private int turretCost;

	public int SpawnPointCount => spawnPoints.Count;

	bool showGrid, showPaths;

	public bool ShowPaths {
		get => showPaths;
		set {
			/*
			showPaths = value;
			if (showPaths) {
				foreach (GameTile tile in tiles) {
					tile.ShowPath();
				}
			}
			else {
				foreach (GameTile tile in tiles) {
					tile.HidePath();
				}
			}
			*/

			showPaths = value;
			if (showPaths)
			{
				int i = 0;
				GameTile[] gameTiles = new GameTile[size.x * size.y];
				gameTiles[i] = tiles[i];
				while (gameTiles[i].NextTileOnPath != null)
                {
					i++;
					gameTiles[i] = gameTiles[i-1].NextTileOnPath;
				}
				for(int y = 1; y< i; y++)
                {
					gameTiles[y].ShowPath();
                }
			}
			else
			{
				foreach (GameTile tile in tiles)
				{
					tile.HidePath();
				}
			}
		}
	}

    public bool ShowGrid
    {
        get => showGrid;
        set
        {
            showGrid = value;
            if (showGrid)
            {
                foreach (GameTile tile in tiles)
                {
                    tile.ShowGrid();
                }
            }
            else
            {
                foreach (GameTile tile in tiles)
                {
                    tile.HideGrid();
                }
            }
        }
    }

	public void Initialize (Vector2Int size, GameTileContentFactory contentFactory) 
	{
		this.size = size;
		this.contentFactory = contentFactory;
        turretToBuild = -1;

		Vector2 offset = new Vector2(
			(size.x - 1) * 0.5f, (size.y - 1) * 0.5f
		);
		tiles = new GameTile[size.x * size.y];
		for (int i = 0, y = 0; y < size.y; y++) {
			for (int x = 0; x < size.x; x++, i++) {
				GameTile tile = tiles[i] = Instantiate(tilePrefab);
                tile.transform.SetParent(transform, false);
				tile.transform.localPosition = new Vector3(
					x - offset.x, 0f, y - offset.y
				);

				if (x > 0) {
					GameTile.MakeEastWestNeighbors(tile, tiles[i - 1]);
				}
				if (y > 0) {
					GameTile.MakeNorthSouthNeighbors(tile, tiles[i - size.x]);
				}

				tile.IsAlternative = (x & 1) == 0;
				if ((y & 1) == 0) {
					tile.IsAlternative = !tile.IsAlternative;
				}

				tile.Content = contentFactory.Get(GameTileContentType.Empty);
			}
		}
		SetStart(tiles[0]);
		SetDestination(tiles[tiles.Length-1]);
	}

	public void SetDestination (GameTile tile) {
		tile.Content = contentFactory.Get(GameTileContentType.Destination);
		FindPaths();
	}

	public void SetStart(GameTile tile)
	{
		tile.Content = contentFactory.Get(GameTileContentType.Start);
		spawnPoints.Add(tile);
	}

	public GameTile GetSpawnPoint(int index)
	{
		return spawnPoints[index];
	}

	public void SetTurretToBuild(GameTileContentType type, int cost)
    {
		turretToBuild = (int) type;
		turretCost = cost;
	}

	public int GetTurretToBuild()
	{
		return turretToBuild;
	}

	public void BuildTurret(GameTile tile)
    {
        if (Player.Money < turretCost)
        {
			turretToBuild = -1;
			CursorManager.Instance.SetCursor();
			return;
        }
        else
        {
			Player.Money -= turretCost;

		}
        if (tile.Content.Type == GameTileContentType.Empty)
        {
			tile.Content = contentFactory.Get((GameTileContentType)GetTurretToBuild());
			if (!FindPaths())
			{
				tile.Content = contentFactory.Get(GameTileContentType.Empty);
				FindPaths();
			}
		}

	}

	public void UpgradeTurret(GameTile tile)
    {
		
	}

	public void SellTurret(GameTile tile)
    {
		Player.Money += Shop.Instance.GetCostTurret(tile.Content.Type)/2;
		tile.Content = contentFactory.Get(GameTileContentType.Empty);
	}

	public GameTile GetTile (Ray ray) {
		if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, 1)) {
			int x = (int)(hit.point.x + size.x * 0.5f);
			int y = (int)(hit.point.z + size.y * 0.5f);
			if (x >= 0 && x < size.x && y >= 0 && y < size.y) {
				return tiles[x + y * size.x];
			}
		}
		return null;
	}

	bool FindPaths () {
		foreach (GameTile tile in tiles) {
			if (tile.Content.Type == GameTileContentType.Destination) {
				tile.BecomeDestination();
				searchFrontier.Enqueue(tile);
			}
			else {
				tile.ClearPath();
			}
		}
		if (searchFrontier.Count == 0) {
			return false;
		}

		while (searchFrontier.Count > 0) {
			GameTile tile = searchFrontier.Dequeue();
			if (tile != null) {
				if (tile.IsAlternative) {
					searchFrontier.Enqueue(tile.GrowPathNorth());
					searchFrontier.Enqueue(tile.GrowPathSouth());
					searchFrontier.Enqueue(tile.GrowPathEast());
					searchFrontier.Enqueue(tile.GrowPathWest());
				}
				else {
					searchFrontier.Enqueue(tile.GrowPathWest());
					searchFrontier.Enqueue(tile.GrowPathEast());
					searchFrontier.Enqueue(tile.GrowPathSouth());
					searchFrontier.Enqueue(tile.GrowPathNorth());
				}
			}
		}


		GameTile tile2 = tiles[0];

		//Pour savoir si il reste un chemin possible
		if (tile2.NextTileOnPath == null)
        {
			return false;
        }

		return true;
	}

	public void GameUpdate()
	{
		for (int i = 0; i < updatingContent.Count; i++)
		{
			updatingContent[i].GameUpdate();
		}
	}

}