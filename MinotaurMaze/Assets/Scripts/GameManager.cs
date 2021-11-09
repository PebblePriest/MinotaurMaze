using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; set; }

    public float waitToRespawn;

    public GameObject EndScreen;

    public GameObject playerPrefab, player2Prefab;

    public GameObject[] playerPrefabs;

    private void Awake()
    {
        instance = this;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        //GameObject playerToSpawn = playerPrefabs[PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]];
        SpawnPlayer();
        PlayerController.instance.transform.position = new Vector2(PlayerPrefs.GetFloat("PlayerX"), PlayerPrefs.GetFloat("PlayerY"));
        Player2Controller.instance.transform.position = new Vector2(PlayerController.instance.transform.position.x -1, PlayerController.instance.transform.position.y);
    }

    // Update is called once per frame
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
        PhotonNetwork.Instantiate(playerPrefab.name, playerPrefab.transform.position, playerPrefab.transform.rotation);
        //PhotonNetwork.Instantiate(player2Prefab.name, player2Prefab.transform.position, player2Prefab.transform.rotation);
    }
}
