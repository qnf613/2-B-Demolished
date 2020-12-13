using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    private BoxCollider2D boxcol;
    private SpriteRenderer sRender;

    private void Awake()
    {
        boxcol = GetComponent<BoxCollider2D>();
        sRender = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Enemy.numberLeft == 0)
        {
            boxcol.enabled = false;
            sRender.enabled = true;
        }
    }
}
