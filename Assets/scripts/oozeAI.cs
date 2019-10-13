using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oozeAI : MonoBehaviour
{
    Transform target;
    Vector3 dir;
    Rigidbody2D body;
    public int health = 10;
    int flashTimer = 0;
    float damageTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(flashTimer == 15)
            GetComponentInChildren<SpriteRenderer>().color = Color.white;
        flashTimer--;
    }

    private void FixedUpdate()
    {
        dir = new Vector3(0, 0, 0);

        if (target.position.x < transform.position.x)
            dir += new Vector3(-.015f, 0, 0);
        if (target.position.x > transform.position.x)
            dir += new Vector3(.015f, 0, 0);
        if (target.position.y < transform.position.y)
            dir += new Vector3(0, -.015f, 0);
        if (target.position.y > transform.position.y)
            dir += new Vector3(0, .015f, 0);

        body.velocity = new Vector2(0,0);
        if(flashTimer  <= 0)
            transform.position += dir;

        //rotate monster
        Vector3 targetPos = target.position;
        float angleOfRotation = Mathf.Atan2(targetPos.y - transform.position.y, targetPos.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angleOfRotation);

        damageTimer -= Time.fixedDeltaTime;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Sword" && damageTimer <= 0)
        {
            health -= 4;
            damageTimer = .1f;
            if (health <= 0)
                Destroy(gameObject);
            SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();
            sr.color = Color.red;
            flashTimer = 20;
        }
    }
}
