using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class Player2BoxMode : MonoBehaviour
{
    public GameObject playerCyclops, myCamera;
    public Animator anim;
    private bool startTimer;
    private float timer;

    PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();

        if (view.IsMine)
        {
            myCamera.SetActive(true);
        }
    }

    private void Update()
    {
        // timer to ensure that the entire animtion plays when going in and out of the box mode.
        if (startTimer)
        {
            anim.SetTrigger("outBox");
            timer += Time.deltaTime;
            if(timer >= 1)
            {
                GameObject playerCyc = PhotonNetwork.Instantiate(playerCyclops.name, transform.position, transform.rotation);
                playerCyc.transform.localScale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }

    // button press to start a timer for going into box mode
    public void Special(InputAction.CallbackContext context)
    {
        if (view.IsMine)
        {
            if (context.performed)
            {
                startTimer = true;
            }
        }
    }
}
