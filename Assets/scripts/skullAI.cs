using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skullAI : MonoBehaviour
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
        //rotate monster
        Vector3 targetPos = target.position;
        float angleOfRotation = Mathf.Atan2(targetPos.y - transform.position.y, targetPos.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angleOfRotation);

        //prevent monster from being pushed by player
        body.velocity = new Vector2(0, 0);

        //move monster
        transform.Translate(new Vector3(.03f, 0, 0));
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Sword")
        {
            Destroy(gameObject);
        }
    }
}
