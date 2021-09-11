using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D theRB;
    private SpriteRenderer theSR;
    private Animator anim;

    public float moveSpeed, jumpForce;
    
    private float inputX;
    private bool canDoubleJump;

    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
        theSR = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        theRB.velocity = new Vector2(inputX * moveSpeed, theRB.velocity.y);

        if(theRB.velocity.x < 0)
        {
            theSR.flipX = true;
        }
        else if (theRB.velocity.x > 0)
        {
            theSR.flipX = false;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        inputX = context.ReadValue<Vector2>().x;
        anim.SetBool("isRunning", true);
        if (context.canceled)
        {
            anim.SetBool("isRunning", false);
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && GroundCheck.instance.isGrounded == true)
        {
            theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
            canDoubleJump = true;
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

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Swung Sword");
        }
    }
}
