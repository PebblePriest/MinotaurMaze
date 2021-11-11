using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class GameManager : MonoBehaviour
{
    public RoomManager room;
    public static GameManager instance { get; set; }

    public float waitToRespawn;

    public GameObject EndScreen;

    public GameObject playerPrefab;

    public GameObject[] playerPrefabs;

    private void Awake()
    {
        instance = this;
        
    }
  
    void Start()
    {
       
    }

   
    void Update()
    {
        
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

    public void EndGame()
    {
        StartCoroutine(EndGameCo());
    }

    private IEnumerator EndGameCo()
    {
        yield return new WaitForSeconds(5);

        EndScreen.SetActive(true);
        Time.timeScale = 0;
    }
    public void SpawnPlayer()
    {

        PhotonNetwork.Instantiate(playerPrefab.name, playerPrefab.transform.position, playerPrefab.transform.rotation, 0);
        //PhotonNetwork.Instantiate(player2Prefab.name, player2Prefab.transform.position, player2Prefab.transform.rotation);
    }
}
