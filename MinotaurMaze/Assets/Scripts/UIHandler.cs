using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
public class UIHandler : MonoBehaviourPunCallbacks
{
    public InputField joinRoomTF;
    public InputField createRoomTF;
    public GameObject roomPanel;
    public GameObject lobbyPanel;
    public Text roomName;
   public void OnClick_JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinRoomTF.text, null);
    }
    public void OnClick_CreateRoom()
    {
        PhotonNetwork.CreateRoom(createRoomTF.text, new RoomOptions { MaxPlayers = 2 }, null);

    }

    public override void OnJoinedRoom()
    {
        print("Room Joined Success");
        PhotonNetwork.LoadLevel(1);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        print("Room Joined Failed" +returnCode+" Message "+message);
    }
}
