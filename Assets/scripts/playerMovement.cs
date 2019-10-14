using System.Collections;
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
    public GameObject swordHitbox, stickHitbox;
    public Light highLight, lowLight, itemLight;
    float timer = 1f;
    float attackSpeed = 0f;
    public Text WeaponCount;

    public Sprite playerStick;
    int itemEquipped = 0;

    float horizontal;
    float vertical;

    private int health = 10;
    public int attackDamage = 1;
    public Text healthText;
    private float damageBoostTimer = 0;
    public GameObject healthParent;
    private Image[] hearts;

    public Image gameOverScreen;
    public Text gameOverText;
    private bool playerDied = false;

    public float runSpeed = 20.0f;

    public Sprite empty;
    public int swordsCollected = 0;
    public Tilemap tilemap;

    //loop sound time
    private float loopTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        hearts = healthParent.GetComponentsInChildren<Image>();
        //GetComponent<MoreAudioClips>().PlayClip(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDied && Input.anyKeyDown)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        timer += Time.deltaTime;
        loopTimer += Time.deltaTime;
        //if (loopTimer >= 30f)
        //{
        //    GetComponent<MoreAudioClips>().PlayClip(0);
        //    loopTimer = 0f;
        //}
        //swing sword
        if (Input.GetMouseButton(0) && timer > 1f && itemEquipped > 0)
        {
            animator.SetTrigger("Attack");

            if(itemEquipped == 1)
                swing = Instantiate(stickHitbox, transform.position, transform.rotation * Quaternion.Euler(0, 0, 270));
            else
                swing = Instantiate(swordHitbox, transform.position, transform.rotation * Quaternion.Euler(0, 0, 270));

            timer = attackSpeed;
        }

        if (timer > .1f + attackSpeed)
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

        
    }


    private void FixedUpdate()
    {
        //move player, camera, and light
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
        camera.transform.position = new Vector3(body.transform.position.x, body.transform.position.y, body.transform.position.z - 20);
        lowLight.transform.position = new Vector3(body.transform.position.x, body.transform.position.y, -5f);
        highLight.transform.position = new Vector3(body.transform.position.x, body.transform.position.y, -8f);
        itemLight.transform.position = new Vector3(body.transform.position.x, body.transform.position.y, -8f);

        //aim player
        Vector3 mouseWorldPos = camera.ScreenToWorldPoint(Input.mousePosition);
        float angleOfRotation = Mathf.Atan2(mouseWorldPos.y - transform.position.y, mouseWorldPos.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angleOfRotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (damageBoostTimer <= 0 && collision.gameObject.tag == "monster" && !playerDied)
        {
            //play oof

            //GetComponent<MoreAudioClips>().PlayClip(0);
            health--;
            hearts[health].color = new Color(1, 1, 1, 0);
            damageBoostTimer = 2;
        }
        if (health <= 0)
        {
            playerDied = true;
            gameOver();
        }
        if (collision.gameObject.tag == "Stick")
        {
            Destroy(collision.gameObject);
            if (itemEquipped == 0)
            {
                itemEquipped = 1;
                animator.SetInteger("item", 1);
            }
        }
        if (collision.gameObject.tag == "mimic")
        {
            Destroy(collision.gameObject);
            health = Mathf.Min(10, health + 1);
            hearts[health - 1].color = new Color(1, 1, 1, 1);
        }
        if (collision.gameObject.tag == "SwordNine")
        {
            itemEquipped = 2;
            attackSpeed = .2f;
            //Destroy(collision.gameObject);
            SpriteRenderer sr = collision.GetComponent<SpriteRenderer>();
            sr.sprite = empty;
            animator.SetInteger("item", 2);
            attackDamage = 2;
            collision.gameObject.tag = "SwordNineEmpty";
            swordsCollected++;
            WeaponCount.text = "Weapons Collected: " + swordsCollected;
            if (swordsCollected >= 8)
            {
                activateExit();
            }
            
        }

        if (collision.gameObject.tag == "winZone")
        {
            win();
        }
    }

    //player takes damage when entering a monster's hitbox
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //contact with monster
        if (damageBoostTimer <= 0 && collision.gameObject.tag == "monster")
        {
            //play oof
            GetComponent<MoreAudioClips>().PlayClip(0);
            health--;
            hearts[health].color = new Color(1, 1, 1, 0);
            damageBoostTimer = 2;
        }
        if (damageBoostTimer <= 0 && collision.gameObject.tag == "swordArm")
        {
            health --;
            hearts[health].color = new Color(1, 1, 1, 0);
            health--;
            hearts[health].color = new Color(1, 1, 1, 0);
            damageBoostTimer = 2;
        }
        if (health <= 0)
        {
            gameOver();
        }




        if(collision.gameObject.tag == "winZone")
        {
            win();
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
        gameOverScreen.color = new Color(0, 0, 0, 1);
        gameOverText.color = new Color(1, 1, 1, 1);
        playerDied = true;
    }

    private void win()
    {
        gameOverScreen.color = new Color(0,0,0,1);
        gameOverText.text = "You Win";
        gameOverText.color = new Color(1,1,1,1);
        playerDied = true;
    }
}