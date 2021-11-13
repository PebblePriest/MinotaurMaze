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
    public RoomManager room;
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
        if (room == null)
        {
            room = this;
        }
        else
        {
            if (room != this)
            {
                Destroy(room.gameObject);
                room = this;
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
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        Debug.Log("Player has left the room!");
        if (PV.IsMine)
        {
            myNumberInRoom = 1;
            PhotonNetwork.NickName = myNumberInRoom.ToString();
        }
    }


}
