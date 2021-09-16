using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;
    public int maxHealth = 100;
    public static int currentHealth;
    public int knockBackForce;

    private Rigidbody2D theRB;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }
    
    public void TakeDamage(int damage)
    {
        knockBackForce = damage;
        currentHealth -= damage;

        PlayerController.instance.KnockBack();
        
        
        //play hurt animation

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
        Debug.Log("Player Died");
    }
}
