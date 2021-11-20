using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FallBlock : MonoBehaviour
{
   
    public void Awake()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
            if (other.gameObject.tag == "Player")
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().TakeDamage(5);
            }
            PhotonNetwork.Destroy(transform.parent.gameObject);
        
            

        
          
    }
  
}
