using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class Player2Controller : MonoBehaviourPunCallbacks
{
    public static Player2Controller instance;

    private Rigidbody2D theRB;
    //private SpriteRenderer theSR;
    public Animator anim;

    public float moveSpeed, jumpForce, attackingMoveSpeed;
    private float movingSpeed;

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

    public GameObject eye, playerCyc, husk;
    private bool isBox, canAct;
    private float actTimer;

    public bool isEye, isCyc, canEnterHusk;

    public GroundCheck ground;

    public GameObject currentHusk;

    PhotonView view;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        view = GetComponent<PhotonView>();
        theRB = GetComponent<Rigidbody2D>();
        canAct = false;
        movingSpeed = moveSpeed;
    }

    void Update()
    {
        if (view.IsMine)
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

            //keep player from spamming the special button
            if (!canAct)
            {
                actTimer += Time.deltaTime;
                if (actTimer >= 1.5)
                {
                    actTimer = 0;
                    canAct = true;
                }
            }

            //manage animations
            anim.SetFloat("xVel", Mathf.Abs(theRB.velocity.x));
            //anim.SetBool("isGrounded", GroundCheck.instance.isGrounded);
            //anim.SetFloat("yVelocity", theRB.velocity.y);

        }
    }

    private void FixedUpdate()
    {
        if (view.IsMine)
        {
            //keep player from flipping between moving and attacking
            if (!isAttacking)
            {
                //ResetMovement();
            }
            else if (isAttacking)
            {
                StopMovement();
            }
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (view.IsMine)
        {
            if (context.performed && canAct)
            {
                inputX = context.ReadValue<Vector2>().x;
            }

            if (context.canceled)
            {
                inputX = 0;
            }
        }

    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (view.IsMine)
        {

            if (canJump)
            {
                if (context.performed && ground.isGrounded == true)
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
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (view.IsMine)
        {
            Debug.Log("cyclops attack!");
            //if (context.performed && GroundCheck.instance.isGrounded && !isAttacking)
            //{
            //    isAttacking = true;

            //    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);


            //    foreach (Collider2D enemy in hitEnemies)
            //    {
            //        if (enemy.gameObject.tag == "Boss")
            //        {
            //            enemy.GetComponent<Enemy>().TakeDamage(comboAttackDamage);
            //        }
            //        if (enemy.gameObject.tag == "Enemy")
            //        {
            //            enemy.GetComponent<CEnemy>().TakeDamage(comboAttackDamage);
            //        }

            //    }


            //}
        }
    }

    public void Special(InputAction.CallbackContext context)
    {
        if (view.IsMine)
        {
            if (context.performed && canAct)
            {
                if (!isBox)
                {
                    anim.SetTrigger("toBox");
                    canAct = false;
                    isBox = true;
                    StopMovement();
                    moveSpeed = 0f;
                    Debug.Log(moveSpeed);
                }
                else if (isBox)
                {
                    anim.SetTrigger("outBox");
                    canAct = false;
                    isBox = false;
                    ResetMovement();
                }
            }
        }
    }

    public void Husk(InputAction.CallbackContext context)
    {
        if (view.IsMine)
        {
            if (isEye)
            {
                if (context.performed && !isBox && canAct)
                {
                    if (canEnterHusk)
                    {
                        //destroyHusk = true;
                        Destroy(currentHusk);
                        playerCyc.transform.position = this.gameObject.transform.position;
                        playerCyc.SetActive(true);
                        this.gameObject.SetActive(false);
                    }
                }
            }

            if (isCyc)
            {
                if (context.performed && !isBox && canAct)
                {
                    //GameObject newHusk = husk[PhotonNetwork.LocalPlayer];
                    GameObject newHusk = PhotonNetwork.Instantiate(husk.name, transform.position, transform.rotation);
                    newHusk.transform.localScale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
                    eye.transform.position = this.gameObject.transform.position;
                    eye.SetActive(true);
                    this.gameObject.SetActive(false);
                }
            }
        }
    }


    public void StopMovement()
    {
        canJump = false;
        moveSpeed = 0f;
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
        if (view.IsMine)
        {
            knockBackCounter = .5f;
            theRB.velocity = new Vector2(0, PlayerHealth.instance.knockBackForce);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isEye)
        {

            if (other.gameObject.tag == "Husk")
            {
                if (other.GetComponent<Husk>().canUse == true)
                {
                    canEnterHusk = true;
                    currentHusk = other.gameObject;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (isEye)
        {

            if (other.gameObject.tag == "Husk")
            {
                currentHusk = null;
                canEnterHusk = false;
            }
        }
    }


    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if (stream.IsWriting)
    //    {
    //        stream.SendNext(eye.activeSelf);
    //        stream.SendNext(playerCyc.activeSelf);
    //        stream.SendNext(husk.activeSelf);

    //    }
    //    else if (stream.IsReading)
    //    {
    //        eye.SetActive((bool)stream.ReceiveNext());
    //        playerCyc.SetActive((bool)stream.ReceiveNext());
    //        husk.SetActive((bool)stream.ReceiveNext());
    //    }
    //}

    [PunRPC]
    public void RPC_Configure(bool eyeBool, bool cycBool, GameObject empHusk)
    {
        isEye = eyeBool;
        isCyc = cycBool;
        husk = empHusk;
    }
    
}
