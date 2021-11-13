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

    
    
    /// <summary>
    /// Join a room that is available
    /// </summary>
    /// <param name="roomName"></param>
    public void OnClick_JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }
    /// <summary>
    /// Create a room for the player to join, auto joins for the player that created it
    /// </summary>
    public void OnClick_CreateRoom()
    {
        PhotonNetwork.CreateRoom(createRoomTF.text, new RoomOptions { MaxPlayers = 2, BroadcastPropsChangeToAll = true }, null);

    }
    /// <summary>
    /// When the player joins the room, the information is brought over and the new panels are activated.
    /// </summary>
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("Room Joined Success");
        lobbyPanel.SetActive(false);
        roomPanel.SetActive(true);
        roomName.text = "Room Name: " + PhotonNetwork.CurrentRoom.Name;
    }
    /// <summary>
    /// Only shows if the player fails to join a room
    /// </summary>
    /// <param name="returnCode"></param>
    /// <param name="message"></param>
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        print("Room Joined Failed" +returnCode+" Message "+message);
    }
    /// <summary>
    /// Update the rooms that are available to join
    /// </summary>
    /// <param name="roomList"></param>
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if(Time.time >= nextUpdateTime)
        {
            UpdateRoomList(roomList);
            nextUpdateTime = Time.time + updateTimer;
        }
        
    }
    /// <summary>
    /// Code behind updating the room list, each time a player makes a new one and if a player leaves one
    /// </summary>
    /// <param name="list"></param>
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
    /// <summary>
    /// Leave the room and return to room list 
    /// </summary>
    public void OnClick_LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();

    }
    /// <summary>
    /// Turn lobby panel back on
    /// </summary>
    public override void OnLeftRoom()
    {
        roomPanel.SetActive(false);
        lobbyPanel.SetActive(true);
    }
    
    
   

}
