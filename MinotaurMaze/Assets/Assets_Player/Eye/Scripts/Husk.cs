using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Husk : MonoBehaviour
{
    private SpriteRenderer theSR;
    public bool canUse;
    private float timer;
    public Player2Controller p2;
    public PhotonView PV;
    public GameObject body;
    private void Start()
    {
        PV = GetComponent<PhotonView>();
        theSR = GetComponent<SpriteRenderer>();
       
        
    }
    /// <summary>
    /// After a wait time, the husk will become available for the player to enter
    /// </summary>
    private void Update()
    {
       
        if (!canUse)
        {
            theSR.color = new Color(0.5f, 0.5f, 0.5f);
            timer += Time.deltaTime;
            if(timer > 3f)
            {
                theSR.color = new Color(1f, 1f, 1f);
                canUse = true;
            }
        }
        
    }
    


}
