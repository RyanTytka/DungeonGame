using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterAI : MonoBehaviour
{
    Transform target;
    Vector3 dir;
    Rigidbody2D body;

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
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Sword")
            Destroy(gameObject);
    }
}
