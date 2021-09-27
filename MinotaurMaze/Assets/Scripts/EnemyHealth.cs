using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GameObject HealthBar;

    void OnTriggerEnter2D(Collider2D other)
    {
            HealthBar.SetActive(true);
            this.gameObject.SetActive(false);
    }
}
