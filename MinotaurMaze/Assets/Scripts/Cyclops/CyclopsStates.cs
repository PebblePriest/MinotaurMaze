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
    protected Rigidbody2D theRB;
    public float visDist = 8f;
    //float shootDist = 7.0f;
    public float speed = 4f;
    public float basicAttack = 2f;

    public static int health;
    /// <summary>
    /// cyclops states for the ai
    /// </summary>
    /// <param name="_npc"></param>
    /// <param name="_anim"></param>
    /// <param name="_player"></param>
    /// <param name="_cyclops"></param>
    /// <param name="_cyclopsR"></param>
    public CyclopsStates(GameObject _npc, Animator _anim, GameObject _player, GameObject _cyclops, SpriteRenderer _cyclopsR, Rigidbody2D _rB)
    {
        npc = _npc;
        anim = _anim;
        stage = EVENT.ENTER;
        player = _player;
        cyclops = _cyclops;
        cyclopsR = _cyclopsR;
        theRB = _rB;

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
    /// <summary>
    /// Logic for finding the player location on the scene
    /// </summary>
    /// <returns></returns>
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
    /// <summary>
    /// If the player is within these parameters, it will attack the player
    /// </summary>
    /// <returns></returns>
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
