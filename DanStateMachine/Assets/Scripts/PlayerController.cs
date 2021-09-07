using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody theRB;
    public float moveSpeed;

    private float inputX;
    private float inputZ;
    
    // Start is called before the first frame update
    void Start()
    {
        theRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        theRB.velocity = new Vector3(inputZ * -moveSpeed, theRB.velocity.y, inputX * moveSpeed);
    }

    public void Movement(InputAction.CallbackContext context)
    {
        inputX = context.ReadValue<Vector2>().x;
        inputZ = context.ReadValue<Vector2>().y;
    }
}
