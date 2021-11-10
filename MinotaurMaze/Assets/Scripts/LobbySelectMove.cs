using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LobbySelectMove : MonoBehaviour
{
    private int position = 1;
    public Transform point1, point2, point3;
    private float inputX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var spartan = MpCharSelect.instance.isSpartan;
        var eye = MpCharSelect.instance.isEye;
        if (position == 0)
        {
            transform.position = Vector3.Lerp(transform.position, point1.position, 5f * Time.deltaTime);
            spartan = true;
        }
        else if (position == 1)
        {
            transform.position = Vector3.Lerp(transform.position, point2.position, 5f * Time.deltaTime);
            spartan = false;
            eye = false;
        }
        else if (position == 2)
        {
            transform.position = Vector3.Lerp(transform.position, point3.position, 5f * Time.deltaTime);
            eye = true;
        }
    }
    public void SelectionMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            inputX = context.ReadValue<Vector2>().x;

            if (position >= 0 && inputX > 0 && position != 2)
            {
                position++;
            }
            else if (position <= 2 && inputX < 0 && position != 0)
            {
                position--;
            }
        }

        if (context.canceled)
        {
            inputX = 0;
        }
    }
}
