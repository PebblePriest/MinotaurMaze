using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PlayerItem : MonoBehaviourPunCallbacks
{
    Image backgroundImage;
    public Color highlightColor;
    public GameObject leftArrowButton, rightArrowButton;

    ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();
    public Image playerAvatar;
    public Sprite[] avatars;
    

    Player player;

    private void Awake()
    {
        backgroundImage = GetComponent<Image>();
    }

    public void SetPlayerInfo(Player _player)
    {
        player = _player;
        UpdatePlayerItem(player);
    }

    // manages options in the player card
    public void ApplyLocalChanges()
    {
        backgroundImage.color = highlightColor;
        leftArrowButton.SetActive(true);
        rightArrowButton.SetActive(true);
    }

    // Goes through the two different playable characters when clicking the arrow, right does the same.
    public void OnClickLeftArrow()
    {
        if((int)playerProperties["playerAvatar"] == 0)
        {

            playerProperties["playerAvatar"] = 1;

            PhotonNetwork.NickName = "Eye";
            
        }
        else
        {

            playerProperties["playerAvatar"] = 0;

            PhotonNetwork.NickName = "Spartan";
           
        }
        
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public void OnClickRightArrow()
    {
        if ((int)playerProperties["playerAvatar"] == 1)
        {
            playerProperties["playerAvatar"] = 0;
            PhotonNetwork.NickName = "Spartan";
           
        }
        else
        {
            playerProperties["playerAvatar"] = 1;
            PhotonNetwork.NickName = "Eye";
            
        }
        
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if(player == targetPlayer)
        {
            UpdatePlayerItem(targetPlayer);
        }
    }

    void UpdatePlayerItem(Player player)
    {
        if (player.CustomProperties.ContainsKey("playerAvatar"))
        {
            playerAvatar.sprite = avatars[(int)player.CustomProperties["playerAvatar"]];
            playerProperties["playerAvatar"] = (int)player.CustomProperties["playerAvatar"];
        }
        else
        {
            playerProperties["playerAvatar"] = 0;
            PhotonNetwork.SetPlayerCustomProperties(playerProperties);
        }
        
    }
}
