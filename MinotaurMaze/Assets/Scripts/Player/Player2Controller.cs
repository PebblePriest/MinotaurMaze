using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class Player2Controller : MonoBehaviour
{
    public static Player2Controller instance;

    private Rigidbody2D theRB;
    //private SpriteRenderer theSR;
    public Animator anim;

    public float moveSpeed, jumpForce;
    private float movingSpeed;
    public float attackingMoveSpeed;

    private float inputX;
    private bool canDoubleJump;

    public bool isAttacking;
    private bool canJump;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public int comboAttackDamage;

    public float knockBackLength;
    private float knockBackCounter;


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
            theRB = GetComponent<Rigidbody2D>();

            movingSpeed = moveSpeed;
    }

    void Update()
    {
            //keeps player from sliding down slopes
            if (inputX == 0 && knockBackCounter <= 0)
            {
                theRB.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            }
            else
            {
                theRB.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
            }

            //Controls movement, lets player move as long as not being knocked back
            if (knockBackCounter <= 0)
            {
                theRB.velocity = new Vector2(inputX * moveSpeed, theRB.velocity.y);

                if (theRB.velocity.x < 0)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else if (theRB.velocity.x > 0)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
            }

            else
            {
                //knock back to left/right
                knockBackCounter -= Time.deltaTime;
                if (transform.localScale == new Vector3(1, 1, 1))
                {
                    theRB.velocity = new Vector2(-1f, theRB.velocity.y);
                }
                else
                {
                    theRB.velocity = new Vector2(+1f, theRB.velocity.y);
                }
            }

            //manage animations
            anim.SetFloat("moveSpeed", Mathf.Abs(theRB.velocity.x));
            anim.SetBool("isGrounded", GroundCheck.instance.isGrounded);
            anim.SetFloat("yVelocity", theRB.velocity.y);
    }

    private void FixedUpdate()
    {
            //keep player from flipping between moving and attacking
            if (!isAttacking)
            {
                ResetMovement();
            }
            else if (isAttacking)
            {
                StopMovement();
            }
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            inputX = context.ReadValue<Vector2>().x;
        }

        if (context.canceled)
        {
            inputX = 0;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (canJump)
        {
            if (context.performed && GroundCheck.instance.isGrounded == true)
            {
                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                //canDoubleJump = true;
            }
            else
            {
                if (context.performed && canDoubleJump)
                {
                    theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                    canDoubleJump = false;
                }
            }
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed && GroundCheck.instance.isGrounded && !isAttacking)
        {
            isAttacking = true;

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);


            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy.gameObject.tag == "Boss")
                {
                    enemy.GetComponent<Enemy>().TakeDamage(comboAttackDamage);
                }
                if (enemy.gameObject.tag == "Enemy")
                {
                    enemy.GetComponent<CEnemy>().TakeDamage(comboAttackDamage);
                }

            }


        }
    }

    public void Special(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Based on host, perform special ability");
        }
    }

    public void Shift(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Force leave host body");
        }
    }

    public void StopMovement()
    {
        canJump = false;
        moveSpeed = attackingMoveSpeed;
    }

    public void ResetMovement()
    {
        canJump = true;
        moveSpeed = movingSpeed;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void KnockBack()
    {
            knockBackCounter = .5f;
            theRB.velocity = new Vector2(0, PlayerHealth.instance.knockBackForce);
    }
}