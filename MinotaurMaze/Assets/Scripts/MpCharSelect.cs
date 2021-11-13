using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Photon.Pun;
using Photon.Realtime;

public class MpCharSelect : MonoBehaviourPunCallbacks, IPunObservable
{
    public MpCharSelect instance;
    
    public GameObject startButton;
    public int position = 1;
    private float inputX;
    public bool isSpartan, isEye;
    public GameObject p1, p2;
    public Transform point1, point2, point3, point4, point5, point6;
    public PhotonView PV;

    public RoomManager room;

    ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();

    private void Awake()
    {
        instance = this;
        PV = GetComponent<PhotonView>();
    }
    public void Start()
    {
        playerProperties["position"] = 1;
    }

    public void Update()
    {
        
        ChangePosition();
       

    }
    
    public void ChangePosition()
    {
        //Debug.Log("I am in the update");

        //Debug.Log("I am player " + room.myNumberInRoom);
        //Debug.Log("The Photon View is Mine!");
        
        if (room.myNumberInRoom == 1)
        {
            Debug.Log("I am player 1!!!!!");
            if ((int)playerProperties["position"] == 0)
            {
                p1.transform.position = Vector3.Lerp(p1.transform.position, point1.position, 5f * Time.deltaTime);
                isSpartan = true;
            }
            else if ((int)playerProperties["position"] == 1)
            {
                p1.transform.position = Vector3.Lerp(p1.transform.position, point2.position, 5f * Time.deltaTime);
                isSpartan = false;
                isEye = false;
            }
            else if ((int)playerProperties["position"] == 2)
            {
                p1.transform.position = Vector3.Lerp(p1.transform.position, point3.position, 5f * Time.deltaTime);
                isEye = true;
            }

            Debug.Log(playerProperties["position"]);
        }
        if (room.myNumberInRoom == 2)
        {
            Debug.Log("I am Player 2!!!");
            if ((int)playerProperties["position"] == 0)
            {
                p2.transform.position = Vector3.Lerp(p2.transform.position, point4.position, 5f * Time.deltaTime);
                isSpartan = true;
            }
            else if ((int)playerProperties["position"] == 1)
            {
                p2.transform.position = Vector3.Lerp(p2.transform.position, point5.position, 5f * Time.deltaTime);
                isSpartan = false;
                isEye = false;
            }
            else if ((int)playerProperties["position"] == 2)
            {
                p2.transform.position = Vector3.Lerp(p2.transform.position, point6.position, 5f * Time.deltaTime);
                isEye = true;
            }
        }
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }
    public void LateUpdate()
    {
        if (isEye == true && isSpartan == true)
        {
            startButton.SetActive(true);
        }
        else
        {
            startButton.SetActive(false);
        }
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(isSpartan);
            stream.SendNext(isEye);
           
        }
        else
        {
            this.isSpartan = (bool)stream.ReceiveNext();
            this.isEye = (bool)stream.ReceiveNext();
            
        }
    }
    public void SelectionMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            inputX = context.ReadValue<Vector2>().x;

            if ((int)playerProperties["position"] >= 0 && inputX > 0 && (int)playerProperties["position"] != 2)
            {
                playerProperties["position"] = position + 1;
            }
            else if ((int)playerProperties["position"] <= 2 && inputX < 0 && (int)playerProperties["position"] != 0)
            {
                playerProperties["position"] = position - 1;
            }
            isSpartan = false;
            isEye = false;
            PhotonNetwork.SetPlayerCustomProperties(playerProperties);
        }

        if (context.canceled)
        {
            inputX = 0;

            PhotonNetwork.SetPlayerCustomProperties(playerProperties);
        }
    }

}
