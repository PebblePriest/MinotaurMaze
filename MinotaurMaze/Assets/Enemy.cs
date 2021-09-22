using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public static int currentHealth;
    public float knockBackForce;
    public GameObject MinoDeadBody, MinoHead;
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
        Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            
            Die();
        }
    }

    void Die()
    {
        GameObject mBody = Instantiate(MinoDeadBody, transform.position, Quaternion.identity);
        mBody.transform.localScale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);

        GameObject mHead = Instantiate(MinoHead, transform.position, Quaternion.identity);
        mHead.transform.localScale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        mHead.GetComponent<Rigidbody2D>().AddForce(transform.up * 12, ForceMode2D.Impulse);
        if(mHead.transform.localScale == new Vector3(1, 1, 1))
        {
            mHead.GetComponent<Rigidbody2D>().AddForce(transform.right * 5, ForceMode2D.Impulse);

        }
        else if(mHead.transform.localScale == new Vector3(-1, 1, 1))
        {
            mHead.GetComponent<Rigidbody2D>().AddForce(transform.right * -5, ForceMode2D.Impulse);

        }
        Destroy(gameObject);

        Debug.Log("Enemy Died");
    }
}
