using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
   [SerializeField]
    public GameObject[] tiles;
    private Camera mainCamera;

    private int TileSize = 1;

    [SerializeField]
    public CameraMovements cameraMovement;

    // Start is called before the first frame update
    void Start()
    {
        CreateTileOnMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateTileOnMap()
    {
        string[] mapData = ReadLevelText();

        int mapXSize = mapData[0].ToCharArray().Length;
        int mapYSize = mapData.Length;

        Vector3 maxTile = Vector3.zero;

        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));

        for (int y = 0; y < mapYSize; y++)
        {
            char[] newTiles = mapData[y].ToCharArray();
            for (int x = 0; x < mapXSize; x++)
            {
                maxTile = PlaceTile(newTiles[x].ToString(),x,y, worldStart);
            }
        }

        //cameraMovement.SetLimits(new Vector3(maxTile.x + TileSize, maxTile.y - TileSize));
    }

    private Vector3 PlaceTile(string tileType, int x, int y, Vector3 worldStart)
    {
        int tileTypeIndex = Int32.Parse(tileType);
        GameObject newTile = Instantiate(tiles[tileTypeIndex]);
        newTile.transform.position = new Vector3(worldStart.x + (TileSize * x), worldStart.y - (TileSize * y), 0);
        return newTile.transform.position;
    }

    private string[] ReadLevelText()
    {
        TextAsset bindData = Resources.Load("Level") as TextAsset;

        string data = bindData.text.Replace(Environment.NewLine, string.Empty);

        return data.Split('-');
    }
}
