using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MpCharSelect : MonoBehaviour
{
    public GameObject p1, p2;
    public Transform point1, point2, point3;
    public int position = 1;
    private float inputX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(position == 0)
        {
            //p1.transform.position = point1.position;
            p1.transform.position = Vector3.Lerp(p1.transform.position, point1.position, 5f * Time.deltaTime);
        }
        else if(position == 1)
        {
            //p1.transform.position = point2.position;
            p1.transform.position = Vector3.Lerp(p1.transform.position, point2.position, 5f * Time.deltaTime);

        }
        else if(position == 2)
        {
            //p1.transform.position = point3.position;
            p1.transform.position = Vector3.Lerp(p1.transform.position, point3.position, 5f * Time.deltaTime);

        }
    }

    public void SelectionMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            inputX = context.ReadValue<Vector2>().x;

            if(position >= 0 && inputX > 0 && position !=2)
            {
                position++;
            }
            else if(position <= 2 && inputX < 0 && position != 0)
            {
                position--;
            }
        }

        if (context.canceled)
        {
            inputX = 0;
        }
    }

    public void StartMulti(InputAction.CallbackContext context)
    {

    }
}
