using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public float runSpeed = 20.0f;

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
            damageBoostTimer -= Time.deltaTime;
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
        if (damageBoostTimer <= 0)
        {
            health--;
            healthText.text = "health: " + health;
            damageBoostTimer = 2;
        }
    }
}