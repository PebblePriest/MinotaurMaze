using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform player;

    //public Transform Background;

    public float minHeight, maxHeight, leftEdge, rightEdge;

    private Vector2 lastPos;
    //public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        lastPos = transform.position;
        //Background = GameObject.FindWithTag("Background").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, offset.z);
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(player.position.x, leftEdge, rightEdge), Mathf.Clamp(player.position.y, minHeight, maxHeight), transform.position.z);

        Vector2 amountToMove = new Vector2(transform.position.x - lastPos.x, transform.position.y - lastPos.y);

        ////Background.position = Background.position + new Vector3(amountToMove.x, amountToMove.y, 0f);
        //Background.position += new Vector3(amountToMove.x, amountToMove.y, 0f) * 0.5f;

        lastPos = transform.position;
    }
}
