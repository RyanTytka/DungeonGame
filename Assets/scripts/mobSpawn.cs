using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class mobSpawn : MonoBehaviour
{
    public GameObject ooze;
    public GameObject ghost;
    public GameObject pickup;
    public GameObject stick;
    public GameObject sword;
    public GameObject mimic;

    public GameObject wall;

    public Tilemap tilemap;
    private BoundsInt bounds;
    TileBase[] allTiles;

    void Start()
    {
        // Player Spawn 8.5, -4.5
        bounds = tilemap.cellBounds;
        allTiles = tilemap.GetTilesBlock(bounds);
        spawnSwords();
        spawnStick();
        spawnMonster(10, 5);
        spawnPickup(5);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void spawnMonster(int numOoze, int numGhost)
    {
        // Player Spawn Area: (8.5, -7) (6.5, -6) (7.5, -6) (8.5, -6) (9.5, -6) (10.5, -6)
        //                    (6.5, -5) (7.5, -5) (8.5, -5) (9.5, -5) (10.5, -5)
        //                    (6.5, -4) (7.5, -4) (8.5, -4) (9.5, -4) (10.5, -4)
        //                    (6.5, -3) (7.5, -3) (8.5, -3) (9.5, -3) (10.5, -3)

        int ranX;
        int ranY;
        bool spawnPoint = false;
        double[] spawnPointsX = { 8.5, 6.5, 7.5, 8.5, 9.5, 10.5, 6.5, 7.5, 8.5, 9.5, 10.5, 6.5, 7.5, 8.5, 9.5, 10.5, 6.5, 7.5, 8.5, 9.5, 10.5};
        double[] spawnPointsY = { -7, -6, -6, -6, -6, -6, -5, -5, -5, -5, -5, -4, -4, -4, -4, -4, -3, -3, -3, -3, -3};

        while (numOoze > 0 || numGhost > 0)
        {
            spawnPoint = false;
            ranX = Random.Range(-13, 26);
            ranY = Random.Range(-36, 14);

            for (int i = 0; i < spawnPointsX.Length; i++)
            {
                if (ranX + 0.5 == spawnPointsX[i] && ranY == spawnPointsY[i])
                {
                    spawnPoint = true;
                    break;
                }
            }

            if (!spawnPoint && numOoze > 0 && allTiles[ranX + 13 + (ranY + 36) * bounds.size.x].name == "floor")
            {
                Debug.Log(ranX + ", " + ranY);
                GameObject monster = Instantiate(ooze, new Vector3(ranX, ranY), Quaternion.identity);
                numOoze--;
            }
            else if (!spawnPoint && numGhost > 0 && allTiles[ranX + 13 + (ranY + 36) * bounds.size.x].name == "floor")
            {
                Debug.Log(ranX + ", " + ranY);
                GameObject monster = Instantiate(ghost, new Vector3(ranX, ranY), Quaternion.identity);
                numGhost--;
            }
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

    private void spawnSwords()
    { 
        // Set Cords: 1(-4.5f, 9.0f)ReCh 2(3.5f, 9f)ReCh 3(16.5f, 10.5f)ReCh 4(23.5f, 1.0f)ReCh 5(19.5f, -12.5f)ReCh 6(15.5f, -13.0f)ReCh 7(18.5f, -25.5f)ReCh 8(11.5f, -26.5f)ReCh 9(2.5f, -29.0f)ReCh 10(-2.5f, -1.5f)ReCh 11(-.5f, -12.5f)ReCh 12(-3.5f, -18.5f)ReCh 13(4.5f, -12.5f)ReCh 14(-5.5f, -31.5f)Ch 15(15.5f, -31.5f)ReCh 16(14.5, -19.0f)ReCh
        int ranNum;
        int numSwords = 9;
        int numMimic = 7;
        Vector2 empty = new Vector2(0f, 0f);
        Vector2[] positions = { new Vector2(-4.5f, 9.0f), new Vector2(3.5f, 9f), new Vector2(16.5f, 10.5f), new Vector2(23.5f, 1.0f), new Vector2(19.5f, -12.5f), new Vector2(15.5f, -13.0f), new Vector2(18.5f, -25.5f), new Vector2(11.5f, -26.5f), new Vector2(2.5f, -29.0f), new Vector2(-2.5f, -1.5f), new Vector2(-.5f, -12.5f), new Vector2(-3.5f, -18.5f), new Vector2(4.5f, -12.5f), new Vector2(-5.5f, -33.5f), new Vector2(15.5f, -31.5f), new Vector2(14.5f, -19.0f)};
        ranNum = Random.Range(0, 16);

        while (numSwords > 0)
        {
            ranNum = Random.Range(0, 16);
            if (allTiles[(int)(positions[ranNum].x + 13) + (int)((positions[ranNum].y + 36) * bounds.size.x)].name == "floor" && positions[ranNum] != empty)
            {
                GameObject notFirstSword = Instantiate(sword, positions[ranNum], Quaternion.identity);
                positions[ranNum] = empty;
                numSwords--;
            }
        }
        while (numMimic > 0)
        {
            ranNum = Random.Range(0, 16);
            if (allTiles[(int)(positions[ranNum].x + 13) + (int)((positions[ranNum].y + 36) * bounds.size.x)].name == "floor" && positions[ranNum] != empty)
            {
                GameObject mimicmonster = Instantiate(mimic, positions[ranNum], Quaternion.identity);
                mimicmonster.tag = "mimic";
                positions[ranNum] = empty;
                numMimic--;
            }
        }

        GameObject[] mimicTriggers = GameObject.FindGameObjectsWithTag("mimicTrigger");
        GameObject[] mimicmonsters = GameObject.FindGameObjectsWithTag("mimic");
        for (int i = 0; i < mimicmonsters.Length; i++)
        {
            for(int j = 0; j < mimicTriggers.Length; j++)
            {
                string mimicStr = mimicmonsters[i].transform.position.ToString();
                string triggerStr = mimicTriggers[j].name.Substring(0, mimicTriggers[j].name.IndexOf("_")) + ", 0.0";

                if (mimicStr.Substring(1, mimicStr.Length - 2) == triggerStr)
                {
                    //Debug.Log(mimicStr.Substring(1, mimicStr.Length - 2) + "==" + triggerStr);
                    mimicTriggers[j].GetComponent<BoxCollider2D>().enabled = true;
                }
            }
        }
    }
}
