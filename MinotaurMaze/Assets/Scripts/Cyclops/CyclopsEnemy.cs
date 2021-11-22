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
        PV.RPC("CyclopsTakeDmg", RpcTarget.AllBuffered, damage);

        if (CcurrentHealth <= 0)
        {
            Die();
        }
    }
    /// <summary>
    /// pass the damage taken over the network
    /// </summary>
    /// <param name="damage"></param>
    [PunRPC]
    void CyclopsTakeDmg(int damage)
    {
        CcurrentHealth -= damage;
        theRB.velocity = new Vector2(knockBackForce, 0f);

    }
    /// <summary>
    /// deletes the cyclops, if it is the master client, it spawns a husk.
    /// </summary>
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
