using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemy : MonoBehaviour
{
    public int maxHealth = 100;
    public static int CcurrentHealth;
    public float knockBackForce;


    private Rigidbody2D theRB;

    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
        CcurrentHealth = maxHealth;

    }

    public void TakeDamage(int damage)
    {
        CcurrentHealth -= damage;
        theRB.velocity = new Vector2(knockBackForce, 0f);

        if (CcurrentHealth <= 0)
        {

            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
        Debug.Log("Enemy Died");
    }
}
