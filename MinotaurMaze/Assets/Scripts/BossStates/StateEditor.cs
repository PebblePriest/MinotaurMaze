using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEditor 
{
    public enum STATE
    {
        IDLE, WALKING, SWIPE, CHARGE, RAGE, 
    };
    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    };

    public STATE name;
    protected EVENT stage;
    protected GameObject npc;
    protected Animator anim;
    protected GameObject player;
    protected StateEditor nextState;
    protected Transform target;
    protected GameObject boss;
    protected SpriteRenderer theBossR;
    public float visDist = 8f;
    //float shootDist = 7.0f;
    public float speed = 1f;
    public float basicAttack = 2f;
    protected GameObject leftSide;
    protected GameObject rightSide;
    public static int health;
    public float closeDist = 10f;
    public StateEditor(GameObject _npc, Animator _anim, GameObject _player, GameObject _boss, SpriteRenderer _theBossR, GameObject _rightSide, GameObject _leftSide)
    {
        npc = _npc;
        anim = _anim;
        stage = EVENT.ENTER;
        player = _player;
        boss = _boss;
        theBossR = _theBossR;
        leftSide = _leftSide;
        rightSide = _rightSide;
        
    }
    public virtual void Enter() { stage = EVENT.UPDATE; }
    public virtual void Update() { stage = EVENT.UPDATE; }
    public virtual void Exit() { stage = EVENT.EXIT; }
    public StateEditor Process()
    {
        if (stage == EVENT.ENTER) Enter();
        if (stage == EVENT.UPDATE) Update();
        if (stage == EVENT.EXIT)
        {
            Exit();
            return nextState;
        }
        return this;
    }
    public bool FindPlayer()
    {

        float dist = Vector2.Distance(player.transform.position, boss.transform.position);
        if(dist < visDist)
        {
            Debug.Log("See Player");
            return true;
            
        }
        Debug.Log("Player Missing");
        return false;
    }
    public bool AttackPlayer()
    {
        float dist = Vector2.Distance(player.transform.position, boss.transform.position);
        if(dist < basicAttack)
        {
            return true;
        }
        return false;
    }
    public bool LeftSideAttack()
    {
        float dist = Vector2.Distance(leftSide.transform.position, boss.transform.position);
        if (dist < basicAttack)
        {
            return true;
        }
        return false;
    }
    public bool RightSideAttack()
    {
        float dist = Vector2.Distance(rightSide.transform.position, boss.transform.position);
        if (dist < basicAttack)
        {
            return true;
        }
        return false;
    }
    public bool ChargeLeftSide()
    {
        float dist = Vector2.Distance(leftSide.transform.position, player.transform.position);
        if(dist < closeDist)
        {
            return true;
        }
        return false;
    }
    public bool ChargeRightSide()
    {
        float dist = Vector2.Distance(rightSide.transform.position, player.transform.position);
        if (dist > closeDist)
        {
            return true;
        }
        return false;
    }



}
