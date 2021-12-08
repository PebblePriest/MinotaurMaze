using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : StateEditor
{
    public float timer;

    public Walking(GameObject _npc, Animator _anim, GameObject _player, GameObject _boss, SpriteRenderer _theBossR, GameObject _rightSide, GameObject _leftSide, Rigidbody2D _rB) : base(_npc, _anim, _player, _boss, _theBossR, _leftSide, _rightSide, _rB)
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
        //boss.transform.position = Vector2.MoveTowards(boss.transform.position, new Vector2(player.transform.position.x, boss.transform.position.y), speed * Time.deltaTime);

        Vector2 velocity = new Vector2(15f, 0f);

        if (player.transform.position.x > boss.transform.position.x)
        {
            npc.transform.localScale = new Vector3(-1, 1, 1);

            theRB.MovePosition(theRB.position + velocity * Time.deltaTime);
        }
        else if (player.transform.position.x < boss.transform.position.x)
        {
            //Going Left
            npc.transform.localScale = new Vector3(1, 1, 1);

            theRB.MovePosition(theRB.position + velocity * Time.deltaTime * -1f);
        }

        //Debug.Log("Is Walking towards player");
        if (AttackPlayer())
        {
            nextState = new Swipe(npc, anim, player, boss, theBossR, leftSide, rightSide, theRB);
            stage = EVENT.EXIT;
        }
        if (timer >= 4)//this needs replaced with player health when available
        {
            timer = 0;
            nextState = new Charge(npc, anim, player, boss, theBossR, leftSide, rightSide, theRB);
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
