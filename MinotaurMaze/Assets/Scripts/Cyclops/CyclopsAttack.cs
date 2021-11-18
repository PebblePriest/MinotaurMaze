using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyclopsAttack : CyclopsStates
{
    public float timer;
    public CyclopsAttack(GameObject _npc, Animator _anim, GameObject _player, GameObject _boss, SpriteRenderer _theBossR) : base(_npc, _anim, _player, _boss, _theBossR)
    {
        name = STATE.ATTACK;
    }
    public override void Enter()
    {
        anim.SetTrigger("isAttacking");
        base.Enter();

    }
    public override void Update()
    {


        timer += Time.deltaTime;

        if (timer >= 1)
        {
            //Debug.Log("timer over");
            timer = 0;
            nextState = new CyclopsIdle(npc, anim, player, cyclops, cyclopsR);
            stage = EVENT.EXIT;
        }


    }

    public override void Exit()
    {
        //Debug.Log("swipe reset");
        anim.ResetTrigger("isAttacking");
        base.Exit();
    }
}
