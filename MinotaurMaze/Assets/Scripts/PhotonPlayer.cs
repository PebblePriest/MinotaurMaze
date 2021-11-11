using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
public class PhotonPlayer : MonoBehaviour
{
    private PhotonView PV;
    public GameObject myAvatar;
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        
        if (PV.IsMine)
        {
            myAvatar = PhotonNetwork.Instantiate(Path.Combine("Player"), transform.position, transform.rotation, 0);
            PlayerController.instance.transform.position = new Vector2(PlayerPrefs.GetFloat("PlayerX"), PlayerPrefs.GetFloat("PlayerY"));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
