using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    //player variables
    private Transform playerTransform;
    private int playerHealth = 10;

    //attack variables
    public Transform attackPos;
    public float attackRange = 2;
    public LayerMask enemies;
    private int attackDamage = 1;
    private float attackCooldown = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && attackCooldown <= 0)
        {
            attack();
        }

        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }
    }

    void attack()
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemies);
        foreach (Collider2D enemy in enemiesToDamage)
        {
            enemy.GetComponent<oozeAI>().health -= attackDamage;
        }
        attackCooldown = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerHealth--;
    }
}
