using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.InputSystem;
public class PlayerItem : MonoBehaviour
{
    
    
    public Image backgroundImage;
    public Color highlightColor;
    public GameObject p1, p2;
    Transform point1, point2, point3, point21, point22, point23;
    public int position = 1;
    private float inputX;
    public bool player1, player2;
    
    
    public void Awake()
    {
        backgroundImage = GetComponent<Image>();
        point1 = GameObject.Find("TransformPnts/p1Pnt1").transform;
        point2 = GameObject.Find("p1Pnt2").transform;
        point3 = GameObject.Find("p1Pnt3").transform;
        point21 = GameObject.Find("p2Pnt1").transform;
        point22 = GameObject.Find("p2Pnt2").transform;
        point23 = GameObject.Find("p2Pt3").transform;
        p1 = GameObject.Find("p1");
        p2 = GameObject.Find("p2");

    }

    
   

   
    


}
