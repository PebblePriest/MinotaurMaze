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
    GameObject leftSide;
    GameObject rightSide;

  
    void Start()
    {
        anim = this.GetComponent<Animator>();
        boss = this.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        leftSide = GameObject.FindGameObjectWithTag("LeftSide");
        rightSide = GameObject.FindGameObjectWithTag("LeftSide");
        theBossR = GetComponent<SpriteRenderer>();
        currentState = new Idle(this.gameObject, anim, player, boss, theBossR, leftSide, rightSide);
        
        

    }
    void Update()
    {
        currentState = currentState.Process();
    }

    
}
