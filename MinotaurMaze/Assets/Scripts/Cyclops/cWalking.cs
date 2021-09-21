using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cWalking : cStates
{
    public float timer;
    public cWalking(GameObject _npc, Animator _anim, GameObject _player, GameObject _boss, SpriteRenderer _theBossR) : base(_npc, _anim, _player, _boss, _theBossR)
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
        cyclops.transform.position = Vector2.MoveTowards(cyclops.transform.position, new Vector2(player.transform.position.x, cyclops.transform.position.y), speed * Time.deltaTime);
        if (player.transform.position.x > cyclops.transform.position.x)
        {
            npc.transform.localScale = new Vector3(-1, 1, 1);
            //Going Right
            //theBossR.flipX = true;
        }
        else if (player.transform.position.x < cyclops.transform.position.x)
        {
            //Going Left
            npc.transform.localScale = new Vector3(1, 1, 1);
            //theBossR.flipX = false;
        }
        //Debug.Log("Is Walking towards player");
        if (AttackPlayer())
        {
            nextState = new cAttack(npc, anim, player, cyclops, cyclopsR);
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
