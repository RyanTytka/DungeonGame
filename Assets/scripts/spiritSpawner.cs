using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spiritSpawner : MonoBehaviour
{
    public GameObject mimic;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            string spiritPosStr = gameObject.name.Substring(0, gameObject.name.IndexOf("_"));
            float xPos = float.Parse(spiritPosStr.Substring(0, spiritPosStr.IndexOf(",")));
            float yPos = float.Parse(spiritPosStr.Substring(spiritPosStr.IndexOf(" ")+1));
            Vector3 spiritPos = new Vector3(xPos, yPos, -3);
            GameObject spirit = Instantiate(mimic, spiritPos, Quaternion.identity);
            spirit.tag = "monster";
            Destroy(gameObject);
        }
    }
}
