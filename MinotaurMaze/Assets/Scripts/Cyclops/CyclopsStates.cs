using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyclopsStates
{
    public enum STATE
    {
        IDLE, WALKING, ATTACK
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
    protected CyclopsStates nextState;
    protected Transform target;
    protected GameObject cyclops;
    protected SpriteRenderer cyclopsR;
    public float visDist = 8f;
    //float shootDist = 7.0f;
    public float speed = 4f;
    public float basicAttack = 2f;

    public static int health;

    public CyclopsStates(GameObject _npc, Animator _anim, GameObject _player, GameObject _cyclops, SpriteRenderer _cyclopsR)
    {
        npc = _npc;
        anim = _anim;
        stage = EVENT.ENTER;
        player = _player;
        cyclops = _cyclops;
        cyclopsR = _cyclopsR;

    }
    public virtual void Enter() { stage = EVENT.UPDATE; }
    public virtual void Update() { stage = EVENT.UPDATE; }
    public virtual void Exit() { stage = EVENT.EXIT; }
    public CyclopsStates Process()
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

        float dist = Vector2.Distance(player.transform.position, cyclops.transform.position);
        if (dist < visDist)
        {
            Debug.Log("See Player");
            return true;

        }
        Debug.Log("Player Missing");
        return false;
    }
    public bool AttackPlayer()
    {
        float dist = Vector2.Distance(player.transform.position, cyclops.transform.position);
        if (dist < basicAttack)
        {
            return true;
        }
        return false;
    }

}
