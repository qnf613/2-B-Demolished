using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anima;
    [SerializeField] GameObject target;
    public int maxHP;
    public int currentHP;
    [SerializeField] private int nextMove;
    private bool notMoving;
    private bool moveUp;
    private bool moveDown;
    private bool moveRight;
    private bool moveLeft;
    [SerializeField] private bool chaser = false;
    [SerializeField] private bool isChasing;

    private void Start()
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

        //movement direction
        switch (nextMove)
        {
            case 0:
                notMoving = true;
                moveUp = false;
                moveDown = false;
                moveLeft = false;
                moveRight = false;
                break;
            case 1:
                notMoving = false;
                moveUp = true;
                moveDown = false;
                moveLeft = false;
                moveRight = false;
                break;
            case 2:
                notMoving = false;
                moveUp = false;
                moveDown = true;
                moveLeft = false;
                moveRight = false;
                break;
            case 3:
                notMoving = false;
                moveUp = false;
                moveDown = false;
                moveLeft = true;
                moveRight = false;
                break;
            case 4:
                notMoving = false;
                moveUp = false;
                moveDown = false;
                moveLeft = false;
                moveRight = true;
                break;
        }

    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        //moving && animations
        if (notMoving)
        {
            rigid.velocity = new Vector2(0, 0);
        }

        if (moveUp)
        {
            rigid.velocity = new Vector2(0, 1f);
        }

        if (moveDown)
        {
            rigid.velocity = new Vector2(0, -1f);
        }

        if (moveRight)
        {
            rigid.velocity = new Vector2(1f, 0);
        }

        if (moveLeft)
        {
            rigid.velocity = new Vector2(-1f, 0);
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
        float nextDirectionTime = Random.Range(2f, 4f);
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
        //
        if (trigger.gameObject.CompareTag("Player") && chaser)
        {
            isChasing = false;
            StartCoroutine("ChangeMovement");
        }
    }
}
