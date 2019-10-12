using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class mobSpawn : MonoBehaviour
{
    public GameObject monster;

    public GameObject wall;

    public Tilemap tilemap;
    private BoundsInt bounds;
    TileBase[] allTiles;

    void Start()
    {
        bounds = tilemap.cellBounds;
        allTiles = tilemap.GetTilesBlock(bounds);
        spawnMonster(5);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void spawnMonster(int num)
    {
        int ranX;
        int ranY;

        for (int i = 0; i < num; i++)
        {
            ranX = Random.Range(-13, 26);
            ranY = Random.Range(-36, 14);

            if(allTiles[ranX + 13 + (ranY + 36) * bounds.size.x].name == "floor")
            {
                Debug.Log(ranX + ", " + ranY);
                GameObject monster2point0 = Instantiate(monster, new Vector3(ranX, ranY), Quaternion.identity);
            } 
            else
            {
                i--;
            }
                //if (ghost > 0)
                //{
                //    dungeon[ranNum1, ranNum2] = "M";
                //    ghost--;
                //}
                //else if (tankMon > 0)
                //{
                //    dungeon[ranNum1, ranNum2] = "T";
                //    tankMon--;
                //}
                //else if (fastMon > 0)
                //{
                //    dungeon[ranNum1, ranNum2] = "F";
                //    fastMon--;
                //}
                //else
                //{
                //    dungeon[ranNum1, ranNum2] = "I";
                //    numItem--;
                //}
        }
    }
}
