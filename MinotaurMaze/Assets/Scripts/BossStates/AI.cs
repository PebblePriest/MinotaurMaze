using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    Animator anim;
    GameObject player;
    StateEditor currentState;
    Transform target;
    GameObject boss;
    SpriteRenderer theBossR;

    void Start()
    {
        anim = this.GetComponent<Animator>();
        boss = this.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        theBossR = GetComponent<SpriteRenderer>();
        currentState = new Idle(this.gameObject, anim, player, boss, theBossR);
        
    }
    void Update()
    {
        currentState = currentState.Process();
    }
}
