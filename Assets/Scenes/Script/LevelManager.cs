using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    public GameObject[] tiles;
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        mainCamera.transform.position = new Vector3(2.5f, 2.5f, -10);
        mainCamera.orthographicSize = 10f;
        CreateTileOnMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateTileOnMap()
    {
        string[] mapData = new string[]
        {
            "1111", "1111", "2222", "3333", "0000"
        };
        int mapXSize = mapData[0].ToCharArray().Length;
        int mapYSize = mapData.Length;

        for (int y = 0; y < mapYSize; y++)
        {
            char[] newTiles = mapData[y].ToCharArray();
            for (int x = 0; x < mapXSize; x++)
            {
                PlaceTile(newTiles[x].ToString(),x,y);
            }
        }
    }

    private void PlaceTile(string tileType, int x, int y)
    {
        int tileTypeIndex = Int32.Parse(tileType);
        GameObject newTile = Instantiate(tiles[tileTypeIndex]);
        newTile.transform.position = new Vector3(x, y, 1);
    }
}
