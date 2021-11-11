using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher launcher;
    public GameObject connectedScreen;
    public GameObject disconnectedScreen;
    public InputField usernameInput;
    public Text buttonText;

    private void Awake()
    {
        launcher = this;//creates singleton for this script for future use
    }
    /// <summary>
    /// Connect to the server with Photon by clicking this button, changes the text to tell that it is connecting.
    /// </summary>
    public void OnClick_ConnecBtn()
    {
        buttonText.text = "Connecting...";
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
    }
    /// <summary>
    /// If it successfully connects to the server join a lobby
    /// </summary>
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }
    /// <summary>
    /// If it does not auto connect, shoot a error screen
    /// </summary>
    /// <param name="cause"></param>
    public override void OnDisconnected(DisconnectCause cause)
    {
        disconnectedScreen.SetActive(true);
    }
    /// <summary>
    /// When connected to the lobby, bring up the new screen for room options
    /// </summary>
    public override void OnJoinedLobby()
    {
        if (disconnectedScreen.activeSelf)
            disconnectedScreen.SetActive(false);
        connectedScreen.SetActive(true);
    }

    
}
