﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    [SerializeField] private float countdown = 3f;
    [SerializeField] private int damage;
    private float toBeObject = 0;
    [SerializeField] private bool isBombTypeP;
    //Component
    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public LayerMask levelMask;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        Invoke("Blowup", countdown);
    }

    private void Update()
    {
        Explosion ex = explosion.GetComponent<Explosion>();
        ex.damage = damage;
        if (isBombTypeP)
        {
            ex.bombTypeP = true;
        }
        
        if (!gameObject.CompareTag("BombJustPlanted"))
        {
            gameObject.layer = 9;
        }

        toBeObject += Time.deltaTime;
        
        if (toBeObject >= 1f)
        {
            gameObject.tag = "Objects";
        }

    }

    private void Blowup()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        StartCoroutine(CreateExplosions(Vector2.up));
        StartCoroutine(CreateExplosions(Vector2.right));
        StartCoroutine(CreateExplosions(Vector2.down));
        StartCoroutine(CreateExplosions(Vector2.left));
        GetComponent<SpriteRenderer>().enabled = false;
        Destroy(gameObject, .5f);

    }

    private IEnumerator CreateExplosions(Vector3 direction)
    {
        if (isBombTypeP)
        {
            for (int i = 1; i < 2; i++)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0, 0, .5f), direction, i, levelMask);

                if (!hit.collider)
                {
                    Instantiate(explosion, transform.position + (i * direction), explosion.transform.rotation);
                }

                else
                {
                    break;
                }

                yield return new WaitForSeconds(.05f);
            }
        }

        else 
        {
            for (int i = 1; i < 3; i++)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0, 0, .5f), direction, i, levelMask);

                if (!hit.collider)
                {
                    Instantiate(explosion, transform.position + (i * direction), explosion.transform.rotation);
                }

                else
                {
                    break;
                }

                yield return new WaitForSeconds(.05f);
            }
        }

    }
}

