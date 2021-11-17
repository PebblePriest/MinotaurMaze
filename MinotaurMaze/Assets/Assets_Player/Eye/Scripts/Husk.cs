using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Husk : MonoBehaviour
{
    private SpriteRenderer theSR;
    public bool canUse;
    private float timer;
    private void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
    }
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
