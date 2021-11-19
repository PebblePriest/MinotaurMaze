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
   
    
    public void Start()
    {
        
        PV = GetComponent<PhotonView>();
        
    }
    
    
    void OnTriggerEnter2D(Collider2D other)
    {
       
            if (PV.IsMine)
            {
                PV.RPC("SetHealth", RpcTarget.AllBuffered);
                
                
                if (PhotonNetwork.IsMasterClient)
                {
                 PhotonNetwork.Instantiate(MinotaurBoss.name, MinoSpawnPoint.position, transform.rotation);
                }
                    PhotonNetwork.Destroy(box);
            }
       




    }
    [PunRPC]
    void SetHealth()
    {
        HealthBar.SetActive(true);
    }
   
    
    
   
}
