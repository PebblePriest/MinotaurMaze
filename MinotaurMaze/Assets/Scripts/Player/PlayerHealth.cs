using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;
    public int maxHealth = 100;
    public static int currentHealth;
    public int knockBackForce;
    private SpriteRenderer theSR;

    public float invincibleLength;
    private float invincibleCounter;

    public Slider healthSlider;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
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
            healthSlider.value = currentHealth;
            //Debug.Log(currentHealth);
            //play hurt animation
            PlayerController.instance.anim.SetTrigger("isHurt");
            
            if (currentHealth <= 0)
            {
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
