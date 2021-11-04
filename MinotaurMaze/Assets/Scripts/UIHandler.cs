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
        UpdatePlayerList();
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
    void UpdatePlayerList()
    {
        foreach( PlayerItem item in playerItemsList)
        {
            Destroy(item.gameObject);
        }
        playerItemsList.Clear();
        if(PhotonNetwork.CurrentRoom == null)
        {
            return;
        }
        
        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            float count = 0;
            count++;
            if (count == 1)
            {
                SetPlayerName(player.Value);
            }
            else if(count == 2)
            {
                SetPlayerName2(player.Value);
            }
            //PlayerItem newPlayerItem = Instantiate(playerItemPrefab, playerItemParent);
            //newPlayerItem.SetPlayerName(player.Value);
            //if(player.Value == PhotonNetwork.LocalPlayer)
            //{
            //    newPlayerItem.ApplyChanges();
            //}
            //playerItemsList.Add(newPlayerItem);
        }

    }
    public void SetPlayerName(Player _player)
    {
        p1.text = _player.NickName;
    }
    public void SetPlayerName2(Player _player)
    {
        p2.text = _player.NickName;
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();

    }
    private void Update()
    {
        if(PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        {
            playBtn.SetActive(true);
        }
        else
        {
            playBtn.SetActive(false);
        }
    }
    public void OnClickPlayButton()
    {
        PhotonNetwork.LoadLevel(1);

    }

}
