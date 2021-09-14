using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : StateEditor
{
    public float timer;
    public Charge(GameObject _npc, Animator _anim, GameObject _player, GameObject _boss, SpriteRenderer _theBossR, GameObject _rightSide, GameObject _leftSide) : base(_npc, _anim, _player, _boss, _theBossR, _leftSide, _rightSide)
    {
        name = STATE.CHARGE;
        speed = 5f;

    }
    public override void Enter()
    {
        //anim.SetTrigger("isCharging");
        base.Enter();
    }
    public override void Update()
    {

        Debug.Log("Charging");
        
      
        
        if (ChargeLeftSide())
        {

            npc.transform.localScale = new Vector3(-1, 1, 1);
              boss.transform.position = Vector2.MoveTowards(boss.transform.position, new Vector2(leftSide.transform.position.x, boss.transform.position.y), speed * Time.deltaTime);
                if(LeftSideAttack())
                {
                timer += Time.deltaTime;

                if (timer >= 1)
                    {
                    Debug.Log("I hit the player!");
                    timer = 0;
                    nextState = new Idle(npc, anim, player, boss, theBossR, leftSide, rightSide);
                    stage = EVENT.EXIT;
                    }
                }
            
            //theBossR.flipX = true;
        }
        else if (ChargeRightSide())
        {
            npc.transform.localScale = new Vector3(1, 1, 1);
            
                boss.transform.position = Vector2.MoveTowards(boss.transform.position, new Vector2(rightSide.transform.position.x, boss.transform.position.y), speed * Time.deltaTime);
            if (RightSideAttack())
            {
                timer += Time.deltaTime;

                if (timer >= 1)
                {
                    Debug.Log("I hit the player!");
                    timer = 0;
                    nextState = new Idle(npc, anim, player, boss, theBossR, leftSide, rightSide);
                    stage = EVENT.EXIT;
                }
            }

            //theBossR.flipX = false;
        }
        
        if (AttackPlayer())
        {
            
           
            timer += Time.deltaTime;

            if (timer >= 1)
            {
                Debug.Log("I hit the player!");
                timer = 0;
                nextState = new Idle(npc, anim, player, boss, theBossR, leftSide, rightSide);
                stage = EVENT.EXIT;
            }
        }
        
    }
    public override void Exit()
    {
        Debug.Log("Switching to non charge");
        speed = 1f;
        anim.ResetTrigger("isCharging");
        base.Exit();
    }

}
