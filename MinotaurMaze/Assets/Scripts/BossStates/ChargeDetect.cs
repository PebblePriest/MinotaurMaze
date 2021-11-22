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
    /// <summary>
    /// if the player is within a certain range, it will either use a swipe or a charge attack for both enemy types.
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (isCharge)
        {
            var ai = AI.instance;
            if (other.gameObject.tag == "Ground")
            {
                foreach (GameObject point in fbPoints)
                {
                    if(PhotonNetwork.IsMasterClient)
                    {
                        PhotonNetwork.Instantiate(FallBlock.name, point.transform.position, point.transform.rotation);
                    }
                    
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
                
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().TakeDamage(SwipeDamage);
            }
        }
    }
}
