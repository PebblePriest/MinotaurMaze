using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public GameObject E1;
    public GameObject E2;
    public GameObject E3;
    public GameObject E4;
    public GameObject E5;

    // Start is called before the first frame update
    void Start()
    {

        E1.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
        Debug.Log(Enemy.currentHealth);
        if (Enemy.currentHealth <= 75)
        {
            E1.SetActive(false);
            E2.SetActive(true);
        }
        if (Enemy.currentHealth <= 50)
        {
            E2.SetActive(false);
            E3.SetActive(true);
        }
        if (Enemy.currentHealth <= 25)
        {
            E3.SetActive(false);
            E4.SetActive(true);
        }
        if (Enemy.currentHealth <= 1)
        {
            E4.SetActive(false);
            E5.SetActive(true);
            
        }
    }
}
