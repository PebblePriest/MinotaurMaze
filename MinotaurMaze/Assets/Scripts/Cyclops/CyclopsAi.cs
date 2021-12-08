using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyclopsAi : MonoBehaviour
{
    public Animator anim;
    public GameObject player;
    public CyclopsStates currentState;
    public Transform target;
    public GameObject cyclops;
    public SpriteRenderer cyclopsR;
    public Rigidbody2D theRB;



    void Start()
    {
        anim = this.GetComponent<Animator>();
        cyclops = this.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        cyclopsR = GetComponent<SpriteRenderer>();
        theRB = GetComponent<Rigidbody2D>();
        currentState = new CyclopsIdle(this.gameObject, anim, player, cyclops, cyclopsR, theRB);



    }
    void Update()
    {
        currentState = currentState.Process();
    }
}
