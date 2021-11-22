using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player2Health : MonoBehaviour
{
    public GameObject myCamera;
    public PhotonView PV;

    public void Start()
    {
        PV = GetComponent<PhotonView>();
    }
    /// <summary>
    /// Sets the camera for player 2
    /// </summary>
    private void Update()
    {
        if(PV.IsMine)
        {
            myCamera.SetActive(true);
        }
       
    }
}
