using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyclopsIdle : CyclopsStates
{
    public float timer;
    public CyclopsIdle(GameObject _npc, Animator _anim, GameObject _player, GameObject _boss, SpriteRenderer _theBossR, Rigidbody2D _rB) : base(_npc, _anim, _player, _boss, _theBossR, _rB)
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
                nextState = new CyclopsWalking(npc, anim, player, cyclops, cyclopsR, theRB);
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
