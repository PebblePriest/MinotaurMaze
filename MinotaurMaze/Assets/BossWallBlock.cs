using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWallBlock : MonoBehaviour
{
    public static BossWallBlock instance;

    public GameObject[] columnParts;

    private BoxCollider2D col;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        col = GetComponent<BoxCollider2D>();
    }

    public void wallDisable()
    {
        col.enabled = !col.enabled;
        foreach(GameObject part in columnParts)
        {
            GetComponent<SpriteRenderer>().color = new Color(.5f, .5f, .5f);
        }
    }
}
