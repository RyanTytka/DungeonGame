using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oozeAI : MonoBehaviour
{
    Transform target;
    Vector3 dir;
    Quaternion facing;
    Rigidbody2D body;
    public int health = 10;
    int flashTimer = 0;

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
            GetComponent<SpriteRenderer>().color = Color.white;
        flashTimer--;
    }

    private void FixedUpdate()
    {
        //rotate monster
        Vector3 targetPos = target.position;
        float angleOfRotation = Mathf.Atan2(targetPos.y - transform.position.y, targetPos.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angleOfRotation);

        //move monster
        if (flashTimer <= 0)
            transform.Translate(new Vector3(.015f, 0, 0));

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Sword")
        {
            //kncokback
            transform.Translate(new Vector3(-.4f,0,0));

            health -= 4;
            if (health <= 0)
                Destroy(gameObject);
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.color = Color.red;
            flashTimer = 20;
        }
    }
}
