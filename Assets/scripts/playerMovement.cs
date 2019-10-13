﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{
    Rigidbody2D body;
    GameObject swing;
    Animator animator;
    public Camera camera;
    public GameObject swordHitbox;
    public Light highLight, lowLight;
    float timer = 1f;

    float horizontal;
    float vertical;

    private int health = 10;
    public Text healthText;
    private float damageBoostTimer = 0;

    public Image gameOverScreen;
    public Text gameOverText;
    private bool playerDied = false;

    public float runSpeed = 20.0f;

    public int swordsCollected = 0;
    public Tilemap tilemap;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        timer += Time.deltaTime;

        //swing sword
        if (Input.GetMouseButton(0) && timer > 1f)
        {
            animator.SetTrigger("Attack");

            swing = Instantiate(swordHitbox, transform.position, transform.rotation * Quaternion.Euler(0,0,270));
            timer = 0f;
        }

        if(timer > .1f)
        {
            Destroy(swing);
        }
            
        //timer for when the player can take damage
        if (damageBoostTimer > 0)
        {
            Color c = GetComponentInChildren<SpriteRenderer>().material.color;
            if (c.a == 1)
            {
                c.a = .3f;
            }
            else
            {
                c.a = 1;
            }
            GetComponentInChildren<SpriteRenderer>().material.color = c;
            damageBoostTimer -= Time.deltaTime;
            if (damageBoostTimer <= 0)
            {
                c.a = 1;
                GetComponentInChildren<SpriteRenderer>().material.color = c;
            }
        }

        if(playerDied && Input.anyKeyDown)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }


    private void FixedUpdate()
    {
        //move player, camera, and light
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
        camera.transform.position = new Vector3(body.transform.position.x, body.transform.position.y, body.transform.position.z - 20);
        lowLight.transform.position = new Vector3(body.transform.position.x, body.transform.position.y, body.transform.position.z - 1);
        highLight.transform.position = new Vector3(body.transform.position.x, body.transform.position.y, body.transform.position.z - 2);

        //aim player
        Vector3 mouseWorldPos = camera.ScreenToWorldPoint(Input.mousePosition);
        float angleOfRotation = Mathf.Atan2(mouseWorldPos.y - transform.position.y, mouseWorldPos.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angleOfRotation);
    }

    //player takes damage when entering a monster's hitbox
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //contact with monster
        if (damageBoostTimer <= 0 && collision.gameObject.tag == "monster")
        {
            health--;
            damageBoostTimer = 2;

        }
        //constact with spirit's attack
        else if (damageBoostTimer <= 0 && collision.gameObject.tag == "swordArm")
        {
            health -= 2;
            damageBoostTimer = 2;

        }
        //update health display
        healthText.text = "health: " + health;
        //check if player's died
        if (health <= 0)
        {
            gameOver();
        }

        if (collision.gameObject.tag == "SwordNine")
        {
            swordsCollected++;
            Destroy(collision.gameObject);
            if(swordsCollected == 8)
            {
                activateExit();
            }
        }
    }

    //open up the exit
    private void activateExit()
    {
        GameObject[] exit = GameObject.FindGameObjectsWithTag("exit");
        foreach (GameObject wall in exit)
        {
            tilemap.SetTile(tilemap.WorldToCell(wall.transform.position), null);
            Destroy(wall);
        }
    }

    //show a game over screen and tell the player to press a button or something to try again
    private void gameOver()
    {
        gameOverScreen.color = new Color(0,0,0,1);
        gameOverText.color = new Color(1,1,1,1);
        healthText.color = new Color(1,1,1,0);
        playerDied = true;
    }
}