using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class map : MonoBehaviour
{
    public GameObject wall;
    public Transform wallsParent;

    private Tilemap tilemap;
    private BoundsInt bounds;
    TileBase[] allTiles;

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        bounds = tilemap.cellBounds;
        allTiles = tilemap.GetTilesBlock(bounds);
        Debug.Log(bounds.ToString());
        buildWalls();
    }

    private void buildWalls()
    {
        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile != null)
                {
                    if (tile.name == "wall")
                    {
                        //Debug.Log("x:" + x + " y:" + y + " tile:" + tile.name);
                        GameObject newWall = Instantiate(wall, new Vector3(x - 12.5f, y - 36, -2.5f), Quaternion.identity, wallsParent);
                        if(newWall.transform.position == new Vector3(26.5f, -25, -2.5f) || newWall.transform.position == new Vector3(26.5f, -24, -2.5f))
                        {
                            newWall.tag = "exit";
                        }
                    }
                }
            }
        }
    }

}
