using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    //Room Info
    public static RoomManager room;
    private PhotonView PV;
    public int currentScene;
    public int playScene;
   
    //Player Info
    private Player[] photonPlayers;
    public int playersInRoom;
    public int myNumberInRoom;

    public int playerInGame;
   



    private void Awake()
    {
        if (RoomManager.room == null)
        {
            RoomManager.room = this;
        }
        else
        {
            if (RoomManager.room != this)
            {
                Destroy(RoomManager.room.gameObject);
                RoomManager.room = this;
            }
        }
        DontDestroyOnLoad(this.gameObject);
        PV = GetComponent<PhotonView>();
    }
    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += LoadScene;
    }
    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
        SceneManager.sceneLoaded -= LoadScene;
    }
     void LoadScene(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene.buildIndex;
        if(currentScene == playScene)
        {
            GameManager.instance.SpawnPlayer();
        }
    }
  
       
    
    public override void OnJoinedRoom()
    {
        photonPlayers = PhotonNetwork.PlayerList;
        playersInRoom = photonPlayers.Length;
        myNumberInRoom = playersInRoom;
        Debug.Log("I am player " + myNumberInRoom);
        PhotonNetwork.NickName = myNumberInRoom.ToString();
      
    }
    public void StartMulti()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;
        PhotonNetwork.LoadLevel(1);
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log("A new player has joined the room");
        photonPlayers = PhotonNetwork.PlayerList;
        playersInRoom++;
        
    }


}