using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class PauseMenu : MonoBehaviour
{
    public Rigidbody2D skull;
    public Rigidbody2D ooze;
    public Rigidbody2D spirit;
    bool paused = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            if (paused)
            {
                Time.timeScale = 1;
                paused = false;
            }
            else
            {
                paused = true;
            }
        }
        if (paused)
        {
            Time.timeScale = 0;
        }
    }
}
