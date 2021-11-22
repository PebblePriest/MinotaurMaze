using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BossWallBlock : MonoBehaviour
{
    public static BossWallBlock instance;

    public GameObject[] columnParts;

    private BoxCollider2D col;

    public PhotonView PV;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        PV = GetComponent<PhotonView>();
        col = GetComponent<BoxCollider2D>();
    }
    /// <summary>
    /// This is activated once the boss is dead, letting the player pass and continue into the level.
    /// </summary>
    public void wallDisable()
    {
        PV.RPC("Wall", RpcTarget.AllBuffered);
    }
    [PunRPC]
    void Wall()
    {
        col.enabled = !col.enabled;
        foreach (GameObject part in columnParts)
        {
            part.GetComponent<SpriteRenderer>().color = new Color(.5f, .5f, .5f);
        }
    }

}
