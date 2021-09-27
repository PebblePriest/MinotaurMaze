using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyclopsAI : MonoBehaviour
{
    public Animator anim;
    public GameObject player;
    public CStates currentState;
    public Transform target;
    public GameObject cyclops;
    public SpriteRenderer cyclopsR;



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
