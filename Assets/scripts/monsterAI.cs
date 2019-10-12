using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterAI : MonoBehaviour
{
    Transform target;
    Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
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

        transform.position += dir;
    }
}
