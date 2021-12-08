using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyclopsWalking : CyclopsStates
{
    public float timer;
    public CyclopsWalking(GameObject _npc, Animator _anim, GameObject _player, GameObject _boss, SpriteRenderer _theBossR, Rigidbody2D _rB) : base(_npc, _anim, _player, _boss, _theBossR, _rB)
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
        //cyclops.transform.position = Vector2.MoveTowards(cyclops.transform.position, new Vector2(player.transform.position.x, cyclops.transform.position.y), speed * Time.deltaTime);
        //Vector3 dir = (player.transform.position - cyclops.transform.position).normalized;

        //theRB.AddForce((player.transform.position - cyclops.transform.position).normalized * speed, ForceMode2D.Impulse);

        Vector2 velocity = new Vector2(10f, 0f);

        if (player.transform.position.x > cyclops.transform.position.x)
        {
            theRB.MovePosition(theRB.position + velocity * Time.deltaTime);

            npc.transform.localScale = new Vector3((float)-1.65, (float)1.65, 1);
            //Going Right
            //theBossR.flipX = true;
        }
        else if (player.transform.position.x < cyclops.transform.position.x)
        {
            theRB.MovePosition(theRB.position + velocity * Time.deltaTime * -1f);

            //Going Left
            npc.transform.localScale = new Vector3((float)1.65, (float)1.65, 1);
            //theBossR.flipX = false;
        }
        //Debug.Log("Is Walking towards player");
        if (AttackPlayer())
        {
            nextState = new CyclopsAttack(npc, anim, player, cyclops, cyclopsR, theRB);
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
