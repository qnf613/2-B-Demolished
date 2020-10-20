using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //component
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anima;
    [SerializeField] GameObject target;
    [SerializeField] private int maxHP;
    public static int currentHP;
    //movement direction related
    [SerializeField] private int nextMove;
    //chasing player related
    [SerializeField] private bool chaser = false;
    [SerializeField] private bool isChasing;

    private void Awake()
    {
        currentHP = maxHP;
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anima = GetComponent<Animator>();
        StartCoroutine("ChangeMovement");
    }

    private void Update()
    {
        //death condition
        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }

    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        //movement direction
        switch (nextMove)
        {
            case 0:
                rigid.velocity = new Vector2(0, 0);
                break;
            case 1:
                rigid.velocity = new Vector2(0, 1f);
                break;
            case 2:
                rigid.velocity = new Vector2(0, -1f);
                break;
            case 3:
                rigid.velocity = new Vector2(1f, 0);
                break;
            case 4:
                rigid.velocity = new Vector2(-1f, 0);
                break;
        }
        
        //animation
        //if (rigid.velocity.normalized.x != 0)
        //{
        //    anima.SetBool("isWalking", true);
        //}
        //else
        //{
        //    anima.SetBool("isWalking", false);
        //}

        if (isChasing)
        {
            Vector3 targetPos = target.transform.position;
            if (Mathf.RoundToInt(targetPos.x) < Mathf.RoundToInt(transform.position.x))
            {
                nextMove = 3;
            }

            else if (Mathf.RoundToInt(targetPos.x) > Mathf.RoundToInt(transform.position.x))
            {
                nextMove = 4;
            }

            else if (Mathf.RoundToInt(targetPos.x) == Mathf.RoundToInt(transform.position.x))
            {
                nextMove = 0;

                if (Mathf.RoundToInt(targetPos.y) > Mathf.RoundToInt(transform.position.y))
                {
                    nextMove = 1;
                }

                else if (Mathf.RoundToInt(targetPos.y) < Mathf.RoundToInt(transform.position.y))
                {
                    nextMove = 2;
                }
            }

        }

    }

    private IEnumerator ChangeMovement()
    {
        //decide direction to move: 0 stop, 1 up, 2 down, 3 left, 4 right
        nextMove = Random.Range(0, 5);
        //makes the enemy's direction deciding more randomly
        float nextDirectionTime = Random.Range(1f, 2f);
        yield return new WaitForSeconds(nextDirectionTime);
        //repeat this method itself
        StartCoroutine("ChangeMovement");
    }


    private void OnTriggerEnter2D(Collider2D trigger)
    {
        //target player - chaser
        if (trigger.gameObject.CompareTag("Player") && chaser)
        {
            target = trigger.gameObject;
            isChasing = true;
            StopCoroutine("ChangeMovement");
        }
    }

    private void OnTriggeStay2D(Collider2D trigger)
    {
        //chase the player
        if (trigger.gameObject.CompareTag("Player") && chaser)
        {
            StopCoroutine("ChangeMovement");
        }
    }

    private void OnTriggerExit2D(Collider2D trigger)
    {
        //end the chasing
        if (trigger.gameObject.CompareTag("Player") && chaser)
        {
            isChasing = false;
            StartCoroutine("ChangeMovement");
        }
    }
}
