using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Photon.Pun;
using Photon.Realtime;

public class MpCharSelect : MonoBehaviourPunCallbacks, IPunObservable
{
    public static MpCharSelect instance;
    public GameObject startButton;
    public int position = 1;
    private float inputX;
    public bool isSpartan, isEye;
    PhotonView view;

    private void Awake()
    {
        instance = this;
    }
    
    public void LateUpdate()
    {
       
        if(isEye == true && isSpartan == true)
        {
            startButton.SetActive(true);
        }
        else
        {
            startButton.SetActive(false);
        }
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(isSpartan);
            stream.SendNext(isEye);
        }
        else
        {
            this.isSpartan = (bool)stream.ReceiveNext();
            this.isEye = (bool)stream.ReceiveNext();
        }
    }

    public void StartMulti()
    {
        PhotonNetwork.LoadLevel(1);
    }
}
