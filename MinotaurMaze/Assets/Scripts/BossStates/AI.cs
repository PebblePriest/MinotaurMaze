using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public static AI instance;
    public Animator anim;
    public GameObject player;
    public StateEditor currentState;
    Transform target;
    public GameObject boss;
    public SpriteRenderer theBossR;
    public GameObject leftSide;
    public GameObject rightSide;
    public Rigidbody2D theRB;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        anim = this.GetComponent<Animator>();
        boss = this.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        rightSide = GameObject.FindGameObjectWithTag("RightSide");
        leftSide = GameObject.FindGameObjectWithTag("LeftSide");
        theBossR = GetComponent<SpriteRenderer>();
        theRB = GetComponent<Rigidbody2D>();
        currentState = new Idle(this.gameObject, anim, player, boss, theBossR, leftSide, rightSide, theRB);
        
        

    }
    void Update()
    {
        currentState = currentState.Process();
    }

    
}
