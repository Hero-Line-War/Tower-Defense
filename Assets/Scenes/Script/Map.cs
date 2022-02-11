using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject mapTile;
    Camera main;
    public GameObject towerTile;
    [SerializeField] public int width;
    [SerializeField] public int height;
    public static List<GameObject> mapTiles = new List<GameObject>();
    public static GameObject StartTile;
    public static GameObject EndTile;

    private void Start()
    {
        main = Camera.main;
        main.transform.position = new Vector3((float)3.5, (float)3.5, -1);
        generateMap();
        DeleteTile(6, 0);
        CreateTower(3, 3);
    }

    private void generateMap()
    {
        for(int h = 0; h < height; h++)
        {
            for(int w = 0; w < width; w++)
            {
                GameObject newTile = Instantiate(mapTile);
                newTile.GetComponent<SpriteRenderer>().color = Color.white;
                mapTiles.Add(newTile);
                newTile.transform.position = new Vector3(w, h,1);
                if (h == 0 && w == 0)
                {
                    StartTile = newTile;
                }
                if (h == height && w == width)
                {
                    EndTile = newTile;
                }
            }
        }
    }

    public void DeleteTile(int w,int h)
    {
        Destroy(mapTiles[(h*height-1)+(w)]);
    }

    public void CreateTower(int w,int h)
    {
        GameObject newTower = Instantiate(towerTile);
        mapTiles.Add(newTower);
        DeleteTile(w,h);
        newTower.transform.position = new Vector3(w-1, h,1);
    }
}
