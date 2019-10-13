using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class spiritAI : MonoBehaviour
{
    Transform target;
    Vector3 dir;
    Rigidbody2D body;
    public int health = 10;

    //Animator field to allow attack and death animation
    Animator animator;
    //timer so enemy attacks every 5
    float timer = 0f;

    //starts facing right
    private bool facingRight = true;
    private bool facingUp = true;

    //monster starts game 'alive'
    private bool alive = true;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 2.5f && alive)
        {
            //perform attack animation
            animator.SetTrigger("Attack");
            //reset timer
            timer = 0f;
        }
    }

    private void FixedUpdate()
    {
        if (alive)
        {
            dir = new Vector3(0, 0, 0);

            //target is to the left
            if (target.position.x < transform.position.x)
            {
                dir += new Vector3(-.02f, 0, 0);
                //they're facing the wrong way
                if (facingRight)
                {
                    FlipX();
                }
            }
            //target is to the right
            else if (target.position.x > transform.position.x)
            {
                dir += new Vector3(.02f, 0, 0);
                if (!facingRight)
                {
                    FlipX();
                }
            }
            //target is below monster
            if (target.position.y < transform.position.y)
            {
                dir += new Vector3(0, -.02f, 0);
                //if (facingUp)
                //{
                //    FlipY();
                //}
            }
            //target is above
            else if (target.position.y > transform.position.y)
            {
                dir += new Vector3(0, .02f, 0);
                //if (!facingUp)
                //{
                //    FlipY();
                //}
            }

            //prevent spirit from being pushed by player
            body.velocity = new Vector2(0, 0);
            //prevent rotation by player
            //transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.position += dir;
        }

        //rotate monster
        Vector3 targetPos = target.position;
        //float angleOfRotation = Mathf.Atan2(targetPos.y - transform.position.y, targetPos.x - transform.position.x) * Mathf.Rad2Deg;

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Sword")
        {
            health -= 4;
            //should play death animation
            if (health <= 0)
            {
                alive = false;
                Death();
            }
        }
    }

    /// <summary>
    /// Flip sprite horizontally
    /// </summary>
    private void FlipX()
    {
        //switch direction monster's facing
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        //flips on x axis
        scale.x *= -1;
        transform.localScale = scale;
    }

    /// <summary>
    /// Flip sprite veritacly
    /// </summary>
    private void FlipY()
    {
        //switch direction monster's facing
        facingUp = !facingUp;
        Vector3 scale = transform.localScale;
        //flips on x axis
        scale.y *= -1;
        transform.localScale = scale;
    }

    /// <summary>
    /// Plays death animation then destroys monster
    /// </summary>
    private void Death()
    {
        animator.SetTrigger("Death");
        //force monster to wait before destroying self
        //Destroy(gameObject);
        //StartCoroutine(DestroyTimer());

    }

    /// <summary>
    /// Waits for the death animation to finish before destroying monster. Animation time is hard coded in
    /// </summary>
    /// <returns></returns>
    private IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
