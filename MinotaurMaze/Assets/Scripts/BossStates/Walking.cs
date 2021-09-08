using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : StateEditor
{
   
    public Walking(GameObject _npc, Animator _anim, GameObject _player, GameObject _boss, SpriteRenderer _theBossR) : base(_npc, _anim, _player, _boss, _theBossR)
    {
        name = STATE.WALKING;

    }
    public override void Enter()
    {
        anim.SetTrigger("isWalking");
        base.Enter();

    }
    public override void Update()
    {

        boss.transform.position = Vector2.MoveTowards(boss.transform.position, new Vector2(player.transform.position.x, boss.transform.position.y), speed * Time.deltaTime);
        if(player.transform.position.x > boss.transform.position.x)
        {
            theBossR.flipX = true;
        }
        else if(player.transform.position.x < boss.transform.position.x)
        {
            theBossR.flipX = false;
        }
        Debug.Log("Is Walking towards player");
        if(AttackPlayer())
        {
            nextState = new Swipe(npc, anim, player, boss, theBossR);
            stage = EVENT.EXIT;
        }
        
       

    }

    public override void Exit()
    {
        anim.ResetTrigger("isWalking");
        base.Exit();
    }
}
