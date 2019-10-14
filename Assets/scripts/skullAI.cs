using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skullAI : MonoBehaviour
{
    Transform target;
    Vector3 dir;
    Rigidbody2D body;
    public int health = 2, flashTimer = 0;
    public float startDelay = 10f;
    Animator animator;

    //is monster alive
    private bool alive = true;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (flashTimer == 0)
            GetComponent<SpriteRenderer>().color = Color.white;
        flashTimer--;

        //add delay bvefore it starts moving
        if ((transform.position - target.position).magnitude < 8)
            startDelay = 0;
        startDelay = Mathf.Max(0,startDelay - Time.deltaTime);
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
        if(startDelay == 0 && alive)
            transform.Translate(new Vector3(.03f, 0, 0));
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Sword")
        {
            health--;
            //all objects destroy themselves at the end of the death animation
            if (health <= 0)
            {
                alive = false;
                animator.SetTrigger("Death");
                //Destroy(gameObject);
                //Destroy(GetComponent<Collider2D>());
                Invoke("DestroyThis", .5f);
            }
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.color = Color.red;
            flashTimer = 10;
        }
    }

    private void DestroyThis()
    {
        Destroy(gameObject);
    }
}
