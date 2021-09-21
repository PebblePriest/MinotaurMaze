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

    public GameObject H1;
    public GameObject H2;
    public GameObject H3;
    public GameObject H4;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        H1.SetActive(true);
    }
    
    public void TakeDamage(int damage)
    {
        knockBackForce = damage;
        currentHealth -= damage;

        PlayerController.instance.KnockBack();

       // Debug.Log(currentHealth);
        //play hurt animation
        if (currentHealth <= 75)
        {
            H1.SetActive(false);
            H2.SetActive(true);
        }
        if (currentHealth <= 50)
        {
            H2.SetActive(false);
            H3.SetActive(true);
        }
        if (currentHealth <= 25)
        {
            H3.SetActive(false);
            H4.SetActive(true);
        }
        if (currentHealth <= 0)
        {
            H4.SetActive(false);
            Die();
        }
       
    }

    void Die()
    {
        Destroy(gameObject);
        Debug.Log("Player Died");
    }
}
