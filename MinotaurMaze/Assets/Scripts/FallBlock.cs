using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FallBlock : MonoBehaviour
{
   
    public void Awake()
    {
        
    }
    /// <summary>
    /// If the player is hit with the block, they take damage, otherwise if it hits the ground it disapears.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        
            if (other.gameObject.tag == "Player")
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().TakeDamage(5);
            }
            PhotonNetwork.Destroy(transform.parent.gameObject);
        
            

        
          
    }
  
}
