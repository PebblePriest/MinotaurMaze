using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RoomItem : MonoBehaviour
{
    public Text roomName;

    LobbyManager manager;

    //UIHandler manager;
    private void Start()
    {
        //manager = FindObjectOfType<UIHandler>();
        manager = FindObjectOfType<LobbyManager>();
    }
    public void SetRoomName(string _roomName)
    {
        roomName.text = _roomName;
    }
    public void OnClickItem()
    {
        //manager.OnClick_JoinRoom(roomName.text);
        manager.JoinRoom(roomName.text);
    }

}
