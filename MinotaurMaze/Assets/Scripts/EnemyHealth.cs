using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public GameObject HealthBar;
    public GameObject MinotaurBoss;
    public Transform MinoSpawnPoint;
    public GameObject box;
    public PhotonView PV;
    private int spawn = 0;
   
    
    public void Start()
    {
        
        PV = GetComponent<PhotonView>();
        
    }
    
    
    void OnTriggerEnter2D(Collider2D other)
    {
       //When hitting the collision box, you spawn the enemy as well as the health bar for the enemy
            if (PV.IsMine)
            {
                PV.RPC("SetHealth", RpcTarget.AllBuffered);
                if(spawn == 0)
                 {
                 if (PhotonNetwork.IsMasterClient)
                 {
                    PhotonNetwork.Instantiate(MinotaurBoss.name, MinoSpawnPoint.position, transform.rotation);
                    spawn = 1;
                    }
                }
                
               
                    PhotonNetwork.Destroy(box);
            }
       




    }
    [PunRPC]
    void SetHealth()
    {
        //set the minotaur health active
        HealthBar.SetActive(true);
    }
   
    
    
   
}
