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

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
        Invoke("Blowup", countdown);
    }

    private void Update()
    {
    }

    private void Blowup()
    {
        Destroy(gameObject);
    }
}
