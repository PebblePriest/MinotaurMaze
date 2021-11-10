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

    public RoomItem roomItemPrefab;
    List<RoomItem> roomItemsList = new List<RoomItem>();
    public Transform contentObject;

    public float updateTimer = 1.5f;
    float nextUpdateTime;

    public List<PlayerItem> playerItemsList = new List<PlayerItem>();
    public PlayerItem playerItemPrefab;
    public Transform playerItemParent;

    public GameObject playBtn;
    public Text p1, p2;
    
   
    public void OnClick_JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }
    public void OnClick_CreateRoom()
    {
        PhotonNetwork.CreateRoom(createRoomTF.text, new RoomOptions { MaxPlayers = 2 }, null);

    }

    public override void OnJoinedRoom()
    {
        print("Room Joined Success");
        lobbyPanel.SetActive(false);
        roomPanel.SetActive(true);
        roomName.text = "Room Name: " + PhotonNetwork.CurrentRoom.Name;
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        print("Room Joined Failed" +returnCode+" Message "+message);
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if(Time.time >= nextUpdateTime)
        {
            UpdateRoomList(roomList);
            nextUpdateTime = Time.time + updateTimer;
        }
        
    }
    void UpdateRoomList(List<RoomInfo> list)
    {
        foreach(RoomItem item in roomItemsList)
        {
            Destroy(item.gameObject);
        }
        roomItemsList.Clear();

        foreach(RoomInfo room in list)
        {
            RoomItem newRoom = Instantiate(roomItemPrefab, contentObject);
            newRoom.SetRoomName(room.Name);
            roomItemsList.Add(newRoom);
        }
    }
    public void OnClick_LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();

    }
    public override void OnLeftRoom()
    {
        roomPanel.SetActive(false);
        lobbyPanel.SetActive(true);
    }
    
    
    public void OnClickPlayButton()
    {
        PhotonNetwork.LoadLevel(1);

    }

}
