using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    Rigidbody2D body;
    public Camera camera;
    public GameObject swordHitbox;
    public Light highLight, lowLight;
    float timer = 1f;
    GameObject swing;

    float horizontal;
    float vertical;

    public float runSpeed = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
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
            swing = Instantiate(swordHitbox, transform.position, transform.rotation * Quaternion.Euler(0,0,90));
            timer = 0f;
        }

        if(timer > .1f)
            Destroy(swing);


        
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
        float angleOfRotation = Mathf.Atan2(mouseWorldPos.y - transform.position.y, mouseWorldPos.x - transform.position.x) * Mathf.Rad2Deg + 180;
        transform.rotation = Quaternion.Euler(0, 0, angleOfRotation);
    }

}