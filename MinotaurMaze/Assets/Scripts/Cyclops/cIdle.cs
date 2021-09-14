using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cIdle : cStates
{
    public cIdle(GameObject _npc, Animator _anim, GameObject _player, GameObject _boss, SpriteRenderer _theBossR) : base(_npc, _anim, _player, _boss, _theBossR)
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
        base.Update();
    }
    public override void Exit()
    {
        anim.ResetTrigger("isIdle");
        base.Exit();
    }
}
