using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;
    public int maxHealth = 100;
    public static int currentHealth;
    public int knockBackForce;
    private SpriteRenderer theSR;

    public float invincibleLength;
    private float invincibleCounter;

    public GameObject H1;
    public GameObject H2;
    public GameObject H3;
    public GameObject H4;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        H1.SetActive(true);
    }

    private void Update()
    {
        if(invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime;

            if(invincibleCounter <= 0)
            {
                theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, 1f);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (invincibleCounter <= 0)
        {
            knockBackForce = damage;
            currentHealth -= damage;

            PlayerController.instance.KnockBack();

            //Debug.Log(currentHealth);
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
            else
            {
                invincibleCounter = invincibleLength;

                //unity handles color values as a float from 0-1
                theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, 0.5f);
            }
        }
       
    }

    void Die()
    {
        Destroy(gameObject);
        Debug.Log("Player Died");
    }
}
