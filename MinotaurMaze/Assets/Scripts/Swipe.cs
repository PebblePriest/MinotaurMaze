using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : StateEditor
{
    public Swipe(GameObject _npc, Animator _anim, GameObject _player, GameObject _boss, SpriteRenderer _theBossR) : base(_npc, _anim, _player, _boss, _theBossR)
    {
        name = STATE.SWIPE;

    }
    public override void Enter()
    {
        anim.SetTrigger("isSwiping");
        base.Enter();

    }
    public override void Update()
    {

        if (AttackPlayer())
        {
            Debug.Log("Attacking");
            nextState = new Idle(npc, anim, player, boss, theBossR);
            stage = EVENT.EXIT;
        }
        

    }

    public override void Exit()
    {
        anim.ResetTrigger("isSwiping");
        base.Exit();
    }

}
