using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
public class ChargeDetect : MonoBehaviour
{
    [Header("Attack Type")]
    public bool isCharge; 
    public bool isSwipe;

    [Header("Attack Values")]
    public int ChargeDamage;
    public int SwipeDamage;

    [Header("Blocks")]
    public GameObject FallBlock;
    public GameObject[] fbPoints;

    
    public void Start()
    {
        fbPoints = GameObject.FindGameObjectsWithTag("BlockSpawns");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (isCharge)
        {
            var ai = AI.instance;
            if (other.gameObject.tag == "Ground")
            {
                foreach (GameObject point in fbPoints)
                {
                    PhotonNetwork.Instantiate(FallBlock.name, point.transform.position, point.transform.rotation);
                }

                ai.anim.ResetTrigger("isCharging");
                ai.currentState = new Idle(ai.gameObject, ai.anim, ai.player, ai.boss, ai.theBossR, ai.leftSide, ai.rightSide);
            }
            if (other.gameObject.tag == "Player")
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().TakeDamage(ChargeDamage);
            }
        }

        if (isSwipe)
        {
            if (other.gameObject.tag == "Player")
            {
                other.gameObject.GetComponent<PlayerHealth>().TakeDamage(SwipeDamage);
                //GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().TakeDamage(SwipeDamage);
            }
        }
    }
}
