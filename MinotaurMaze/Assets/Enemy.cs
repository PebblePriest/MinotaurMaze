using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public static int currentHealth;
    public float knockBackForce;


    private Rigidbody2D theRB;

    void Start()
    {

        theRB = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
       
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        theRB.velocity = new Vector2(knockBackForce, 0f);
    
        if (currentHealth <= 0)
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