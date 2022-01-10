using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HitEffect : MonoBehaviour
{
    PhotonView view;
    private void Start()
    {
        view = GetComponent<PhotonView>();
    }
    public void DestroyThisEffect()
    {
        PhotonNetwork.Destroy(gameObject);
    }
}
