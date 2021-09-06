using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    Animator anim;
    public Transform player;
    StateEditor currentState;

    //void Start()
    //{
    //    anim = this.GetComponent<Animator>();
    //    currentState = new Idle(this.gameObject, anim, player);
    //}
    // void Update()
    //{
    //    currentState = currentState.Process();
    //}
}
