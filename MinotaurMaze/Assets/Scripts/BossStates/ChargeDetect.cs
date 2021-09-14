using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChargeDetect : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        var ai = AI.instance;
        if(other.gameObject.tag == "Ground")
        {
            
            ai.currentState = new Idle(ai.gameObject, ai.anim, ai.player, ai.boss, ai.theBossR, ai.leftSide, ai.rightSide);
        }
    }
}
