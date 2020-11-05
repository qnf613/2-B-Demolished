using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    private Animator anima;
    private float h;
    private float v;

    private void Awake()
    {
        anima = GetComponent<Animator>();
    }

    private void Update()
    {
        //receive the input to turn on the animation
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        //Animation
        if (anima.GetInteger("hAxisRaw") != h)
        {
            anima.SetBool("isChange", true);
            anima.SetInteger("hAxisRaw", (int)h);
        }
        else if (anima.GetInteger("vAxisRaw") != v)
        {
            anima.SetBool("isChange", true);
            anima.SetInteger("vAxisRaw", (int)v);
        }
        else
        {
            anima.SetBool("isChange", false);
        }
    }
}
