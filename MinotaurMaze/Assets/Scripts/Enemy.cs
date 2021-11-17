using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public static int currentHealth;
    public float knockBackForce;
    public GameObject MinoDeadBody, MinoHead;
    private Rigidbody2D theRB;
    public static bool isBoss;

    public Slider HealthBar;

    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
       
       
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        theRB.velocity = new Vector2(knockBackForce, 0f);
        HealthBar.value = currentHealth;
        Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            
            Die();
        }
    }

    void Die()
    {
        if(gameObject.tag == "Boss")
        {
            GameObject mBody = PhotonNetwork.Instantiate(MinoDeadBody.name, transform.position, Quaternion.identity);
            mBody.transform.localScale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);

            GameObject mHead = PhotonNetwork.Instantiate(MinoHead.name, transform.position, Quaternion.identity);
            mHead.transform.localScale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            mHead.GetComponent<Rigidbody2D>().AddForce(transform.up * 12, ForceMode2D.Impulse);
            if (mHead.transform.localScale == new Vector3(1, 1, 1))
            {
                mHead.GetComponent<Rigidbody2D>().AddForce(transform.right * 5, ForceMode2D.Impulse);

            }
            else if (mHead.transform.localScale == new Vector3(-1, 1, 1))
            {
                mHead.GetComponent<Rigidbody2D>().AddForce(transform.right * -5, ForceMode2D.Impulse);

            }
        }
        
        Destroy(gameObject);
        GameManager.instance.EndGame();
        Debug.Log("Enemy Died");
    }
}
