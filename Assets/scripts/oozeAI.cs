using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oozeAI : MonoBehaviour
{
    Transform target;
    Vector3 dir;
    Quaternion facing;
    Rigidbody2D body;
    public int health = 3;
    int flashTimer = 0;

    //is monster alive
    private bool alive = true;

    //allows playing of animation
    Animator animator;

    //holds sound
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(flashTimer == 15)
            GetComponent<SpriteRenderer>().color = Color.white;
        flashTimer--;
        ////plays on hit sound
        //GetComponent<MoreAudioClips>().PlayClip(0);
    }

    private void FixedUpdate()
    {
        //rotate monster
        Vector3 targetPos = target.position;
        float angleOfRotation = Mathf.Atan2(targetPos.y - transform.position.y, targetPos.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angleOfRotation);

        //prevent monster from being pushed by player
        body.velocity = new Vector2(0, 0);

        //move monster
        body.velocity = new Vector2(0, 0);
        if (flashTimer <= 0 && alive)
            transform.Translate(new Vector3(.015f, 0, 0));

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Sword")
        {
            //plays on hit sound
            GetComponent<MoreAudioClips>().PlayClip(1);
            //kncokback
            transform.Translate(new Vector3(-.4f,0,0));

            GameObject player = GameObject.Find("player");
            playerMovement script = player.GetComponent<playerMovement>();
            health -= script.attackDamage;
            if (health <= 0)
            {
                alive = false;
                animator.SetTrigger("Death");
            }
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.color = Color.red;
            flashTimer = 20;
        }
    }
}
