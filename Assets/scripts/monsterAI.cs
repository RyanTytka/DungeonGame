using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterAI : MonoBehaviour
{
    Transform target;
    Vector3 dir;
    Rigidbody2D body;
    int health = 10;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        dir = new Vector3(0, 0, 0);

        if (target.position.x < transform.position.x)
            dir += new Vector3(-.02f, 0, 0);
        if (target.position.x > transform.position.x)
            dir += new Vector3(.02f, 0, 0);
        if (target.position.y < transform.position.y)
            dir += new Vector3(0, -.02f, 0);
        if (target.position.y > transform.position.y)
            dir += new Vector3(0, .02f, 0);

        body.velocity = new Vector2(0,0);
        transform.position += dir;

        //rotate monster
        Vector3 targetPos = target.position;
        float angleOfRotation = Mathf.Atan2(targetPos.y - transform.position.y, targetPos.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angleOfRotation);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Sword")
        {
            health -= 4;
            if(health <= 0)
                Destroy(gameObject);
        }
    }
}
