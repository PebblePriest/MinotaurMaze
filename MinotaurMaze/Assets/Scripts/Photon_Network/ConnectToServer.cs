using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    public Text buttonText;
    
    // button click function for connecting
    public void OnClickConnect()
    {
        buttonText.text = "Connecting...";
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
    }

    // connect to lobby
    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene("Lobby");
    }
}
