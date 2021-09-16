using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : StateEditor
{
    public float timer;
    
    public Walking(GameObject _npc, Animator _anim, GameObject _player, GameObject _boss, SpriteRenderer _theBossR, GameObject _rightSide, GameObject _leftSide) : base(_npc, _anim, _player, _boss, _theBossR, _leftSide, _rightSide)
    {
        name = STATE.WALKING;

    }
    public override void Enter()
    {
        anim.SetTrigger("isWalking");
        timer = 0;
        base.Enter();

    }
    public override void Update()
    {

       
        
        
            boss.transform.position = Vector2.MoveTowards(boss.transform.position, new Vector2(player.transform.position.x, boss.transform.position.y), speed * Time.deltaTime);
            if (player.transform.position.x > boss.transform.position.x)
            {
                npc.transform.localScale = new Vector3(-1, 1, 1);
            //Going Right
                //theBossR.flipX = true;
            }
            else if (player.transform.position.x < boss.transform.position.x)
            {
           //Going Left
                npc.transform.localScale = new Vector3(1, 1, 1);
                //theBossR.flipX = false;
            }
            //Debug.Log("Is Walking towards player");
            if (AttackPlayer())
            {
                nextState = new Swipe(npc, anim, player, boss, theBossR, leftSide, rightSide);
                stage = EVENT.EXIT;
            }
            if (timer >= 4)//this needs replaced with player health when available
            {
                timer = 0;
                nextState = new Charge(npc, anim, player, boss, theBossR, leftSide, rightSide);
                stage = EVENT.EXIT;
            }
           
        timer += Time.deltaTime;
    }

    public override void Exit()
    {
        anim.ResetTrigger("isWalking");
        base.Exit();
    }
}
