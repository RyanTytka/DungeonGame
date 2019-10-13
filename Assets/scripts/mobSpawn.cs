using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class mobSpawn : MonoBehaviour
{
    public GameObject monster;
    public GameObject pickup;
    public GameObject stick;

    public GameObject wall;

    public Tilemap tilemap;
    private BoundsInt bounds;
    TileBase[] allTiles;

    void Start()
    {
        // Player Spawn 22, 32
        bounds = tilemap.cellBounds;
        allTiles = tilemap.GetTilesBlock(bounds);
        spawnMonster(5);
        //spawnPickup(5);
        spawnStick();
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

            if (allTiles[ranX + 13 + (ranY + 36) * bounds.size.x].name == "floor")
            {
                Debug.Log(ranX + ", " + ranY);
                GameObject monster2point0 = Instantiate(monster, new Vector3(ranX, ranY, -2.5f), Quaternion.identity);
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

    private void spawnPickup(int num)
    {
        int ranX;
        int ranY;
        for (int i = 0; i < num; i++)
        {
            ranX = Random.Range(-13, 26);
            ranY = Random.Range(-36, 14);

            if (allTiles[ranX + 13 + (ranY + 36) * bounds.size.x].name == "floor")
            {
                Debug.Log(ranX + ", " + ranY);
                GameObject pickupAgain = Instantiate(pickup, new Vector3(ranX, ranY), Quaternion.identity);
            }
            else
            {
                i--;
            }
        }
    }
    private void spawnStick()
    {
        // Set Cords: (4.5, -7.5) (3.5, -9.5) (14.5, -8.5) (11.5, -8.5) (4.5, -1.5) (0.5, -1.5)
        int ranNum;
        Vector2[] positions = {new Vector2(4.5f, -7.5f), new Vector2(3.5f, -9.5f), new Vector2(14.5f, -8.5f), new Vector2(11.5f, -8.5f), new Vector2(4.5f, -1.5f), new Vector2(0.5f, -1.5f)};

        ranNum = Random.Range(0, 6);
        
        GameObject starterStick = Instantiate(stick, positions[ranNum], Quaternion.identity);
    }
}
