using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; set; }
    public float waitToRespawn;
    public GameObject[] playerPrefabs;
    public GameObject[] enemySpawnPoints;
    public GameObject CyclopsPrefab;
    
   

    private void Awake()
    {
        instance = this;  
    }
  
    void Start()
    {
        GameObject playerToSpawn = playerPrefabs[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]];
        PhotonNetwork.Instantiate(playerToSpawn.name, CheckPointController.instance.spawnPoint, Quaternion.identity);
      
        if(PhotonNetwork.IsMasterClient)
        {
            foreach (GameObject enemy in enemySpawnPoints)
            {
                PhotonNetwork.Instantiate(CyclopsPrefab.name, enemy.transform.position, transform.rotation);
            }
        }
       
    }
    public void RespawnPlayer()
    {
        StartCoroutine(RespawnCo());
    }

    private IEnumerator RespawnCo()
    {
        yield return new WaitForSeconds(waitToRespawn);

        PlayerHealth.instance.ResetHealth();

        PlayerController.instance.transform.position = CheckPointController.instance.spawnPoint;
    }
}
