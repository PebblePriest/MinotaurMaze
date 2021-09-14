using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Idle : StateEditor
{
    public float timer = 0;
    public float rageActive = 0;
    
    public Idle(GameObject _npc, Animator _anim, GameObject _player, GameObject _boss, SpriteRenderer _theBossR, GameObject _rightSide, GameObject _leftSide) : base(_npc, _anim, _player, _boss, _theBossR,  _leftSide, _rightSide)
    {
        name = STATE.IDLE;
    }
    public override void Enter()
    {
        anim.SetTrigger("isIdle");
        
        base.Enter();

    }
    public override void Update()
    {
       
       
            
            timer += Time.deltaTime;
            health = Enemy.currentHealth;

            if (timer > 2f)
            {
            
                //Debug.Log("Idle Working");
                if (FindPlayer())
                {
                    nextState = new Walking(npc, anim, player, boss, theBossR, leftSide, rightSide);
                    timer = 0;
                    stage = EVENT.EXIT;
                }
                else
                {

                    base.Update();
                }
            }
                 
                

            
          
       



    }

    public override void Exit()
    {
        anim.ResetTrigger("isIdle");
        base.Exit();
    }
}
