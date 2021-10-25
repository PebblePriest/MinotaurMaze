using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CIdle : CStates
{
    public float timer;
    public CIdle(GameObject _npc, Animator _anim, GameObject _player, GameObject _boss, SpriteRenderer _theBossR) : base(_npc, _anim, _player, _boss, _theBossR)
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
        //health = CEnemy.CcurrentHealth;

        if (timer > 2f)
        {

            //Debug.Log("Idle Working");
            if (FindPlayer())
            {
                nextState = new CWalking(npc, anim, player, cyclops, cyclopsR);
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
