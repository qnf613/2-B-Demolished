using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private GameObject player;
    //Component
    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private float countdown = 3f;
    private float toBeObject = 0;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        Invoke("Blowup", countdown);
    }

    private void Update()
    {
        if (!gameObject.CompareTag("BombJustPlanted"))
        {
            gameObject.layer = 9;
        }

        toBeObject += Time.deltaTime;
        
        if (toBeObject >= 1)
        {
            gameObject.tag = "Objects";
        }

    }

    private void Blowup()
    {
        Destroy(gameObject);
    }


}
