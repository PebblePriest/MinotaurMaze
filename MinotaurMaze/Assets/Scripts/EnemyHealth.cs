using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemyHealth : MonoBehaviour
{
    public GameObject HealthBar;
    public GameObject MinotaurBoss;
    public Transform MinoSpawnPoint;

    void OnTriggerEnter2D(Collider2D other)
    {
            HealthBar.SetActive(true);
            this.gameObject.SetActive(false);
            PhotonNetwork.Instantiate(MinotaurBoss.name, MinoSpawnPoint.position, transform.rotation);
    }
}
