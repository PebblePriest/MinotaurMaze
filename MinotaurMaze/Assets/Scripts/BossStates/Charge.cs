using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : StateEditor
{
    public float timer;
    public bool goRight;
    public bool goLeft;
    public Charge(GameObject _npc, Animator _anim, GameObject _player, GameObject _boss, SpriteRenderer _theBossR, GameObject _rightSide, GameObject _leftSide, Rigidbody2D _rB) : base(_npc, _anim, _player, _boss, _theBossR, _leftSide, _rightSide, _rB)
    {
        name = STATE.CHARGE;
        speed = 20f;

    }
    /// <summary>
    /// Charge state for the minotaur, looking for the direction of the player and then going that direction if he has not interected with him for a certain amount of time.
    /// </summary>
    public override void Enter()
    {
        anim.SetTrigger("isCharging");
        timer = 0;
        if (ChargeLocation())
        {
            goRight = true;
            goLeft = false;
            

        }
        if (!ChargeLocation())
        {
            goRight = false;
            goLeft = true;
           
        }
        base.Enter();
    }
    public override void Update()
    {
        Vector2 velocity = new Vector2(15f, 0f);

        if (goRight)
        {
            Debug.Log("Charge Right");
            boss.transform.position = Vector2.MoveTowards(boss.transform.position, new Vector2(rightSide.transform.position.x, boss.transform.position.y), speed * Time.deltaTime);

        }
        else if(goLeft)
        {
            Debug.Log("Charge Left");
            boss.transform.position = Vector2.MoveTowards(boss.transform.position, new Vector2(leftSide.transform.position.x, boss.transform.position.y), speed * Time.deltaTime);

        }



        //npc.transform.localScale = new Vector3(-1, 1, 1);

    }

    //npc.transform.localScale = new Vector3(1, 1, 1);
    //Debug.Log("Going Left");
    //boss.transform.position = Vector2.MoveTowards(boss.transform.position, new Vector2(leftSide.transform.position.x, boss.transform.position.y), speed * Time.deltaTime);

    //if (LeftSideAttack())
    //{
    //    timer += Time.deltaTime;

    //    if (timer >= 1)
    //    {
    //        Debug.Log("I hit the player!");
    //        timer = 0;
    //        nextState = new Idle(npc, anim, player, boss, theBossR, leftSide, rightSide);
    //        stage = EVENT.EXIT;
    //    }
    //}

            //if (Mathf.Approximately(rightSide.transform.position.x, boss.transform.position.x))
            //{
            //    timer += Time.deltaTime;

            //    if (timer >= 1)
            //    {
            //        Debug.Log("I hit the player!");
            //        timer = 0;
            //        nextState = new Idle(npc, anim, player, boss, theBossR, leftSide, rightSide);
            //        stage = EVENT.EXIT;
            //    }
            //}





    public override void Exit()
    {
        Debug.Log("Switching to non charge");
        speed = 1f;
        anim.ResetTrigger("isCharging");
        base.Exit();
    }
}

