using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CyclopsEnemy : MonoBehaviour
{
    public int maxHealth = 100;
    public int CcurrentHealth;
    public float knockBackForce;
    public GameObject cyclopsHusk;
    private PhotonView PV;

    private Rigidbody2D theRB;

    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
        PV = GetComponent<PhotonView>();
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
        if(PV.IsMine)
        {
            Debug.Log("Enemy Died");
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Instantiate(cyclopsHusk.name, this.transform.position, this.transform.rotation);
            }
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
