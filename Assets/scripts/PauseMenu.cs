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
    public GameObject pauseMenu;
    private Image pauseScreen;

    // Start is called before the first frame update
    void Start()
    {
        pauseScreen = pauseMenu.GetComponentInChildren<Image>();
        pauseScreen.color = new Color(1, 1, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            if (paused)
            {
                pauseScreen.color = new Color(1, 1, 1, 0);
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
            pauseScreen.color = new Color(1, 1, 1, 1);
            Time.timeScale = 0;
        }
    }
}
