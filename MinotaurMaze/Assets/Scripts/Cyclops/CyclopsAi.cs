using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyclopsAI : MonoBehaviour
{
    Animator anim;
    GameObject player;
    CStates currentState;
    Transform target;
    GameObject cyclops;
    SpriteRenderer cyclopsR;



    void Start()
    {
        anim = this.GetComponent<Animator>();
        cyclops = this.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        cyclopsR = GetComponent<SpriteRenderer>();
        currentState = new CIdle(this.gameObject, anim, player, cyclops, cyclopsR);



    }
    void Update()
    {
        currentState = currentState.Process();
    }
}
