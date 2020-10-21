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
    //other values
    [SerializeField] private int maxHP;
    public static int currentHP;
    [SerializeField] private int speed = 1;
    //movement direction related
    [SerializeField] private int nextMove;
    //target player related
    [SerializeField] private bool isTargeting;
    [SerializeField] private bool chaser = false;
    [SerializeField] private bool charger = false;
    [SerializeField] private float chargeDuration = 2f;
    [SerializeField]private float holding = 0;
    [SerializeField] private float charging = 0;
    [SerializeField] private bool isHolding = false;
    [SerializeField] private bool isCharging = false;
    //1 = up, 2 = down, 3 = right, 4 = left
    private int chargeDirection;
    [SerializeField] private bool bomber = false;

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

        //tracking: is this enemy targeting player?
        if (!isTargeting)
        {
            isHolding = false;
            isCharging = false;
        }
        //tracking: is this charger holding/charging?
        if (!isHolding)
        {
            holding = 0;
        }
        else if (isHolding)
        {
            holding += Time.deltaTime;
        }
        if (!isCharging)
        {
            chargeDirection = 0;
            speed = 1;
            charging = 0;
        }
        else if (isCharging)
        {
            charging += Time.deltaTime;
        }
       
    }

    private void FixedUpdate()
    {
        Move();
        
        if (isTargeting)
        {
            if (chaser)
            {
                Chase();
            }

            else if (charger)
            {
                Charge();
            }

            else if (bomber)
            {

            }
        }
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
                rigid.velocity = new Vector2(0, speed);
                break;
            case 2:
                rigid.velocity = new Vector2(0, -speed);
                break;
            case 3:
                rigid.velocity = new Vector2(speed, 0);
                break;
            case 4:
                rigid.velocity = new Vector2(-speed, 0);
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

    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        //target the player 
        if (trigger.gameObject.CompareTag("Player"))
        {
            target = trigger.gameObject;
            isTargeting = true;
            StopCoroutine("ChangeMovement");
        }
    }

    private void OnTriggeStay2D(Collider2D trigger)
    {
        //keep target the player
        if (trigger.gameObject.CompareTag("Player"))
        {
            isTargeting = true;
            StopCoroutine("ChangeMovement");
        }
    }

    private void OnTriggerExit2D(Collider2D trigger)
    {
        //end the targeting
        if (trigger.gameObject.CompareTag("Player"))
        {
            isTargeting = false;
            StartCoroutine("ChangeMovement");
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

    //ability of each type of enemy
    private void Chase()
    {
        Vector3 targetPos = target.transform.position;
        if (Mathf.RoundToInt(targetPos.x) > Mathf.RoundToInt(transform.position.x))
        {
            nextMove = 3;
        }

        else if (Mathf.RoundToInt(targetPos.x) < Mathf.RoundToInt(transform.position.x))
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

    private void Charge()
    {
        Vector3 targetPos = target.transform.position;
        Vector3 myPos = gameObject.transform.position;

        isHolding = true;
        if (holding < 2f)
        {
            Debug.ClearDeveloperConsole();
            Debug.Log("holding...");
            rigid.velocity = new Vector2(0, 0);

            if (Mathf.Abs(targetPos.y - myPos.y) > Mathf.Abs(targetPos.x - myPos.x))
            {
                if (Mathf.RoundToInt(targetPos.y) > Mathf.RoundToInt(myPos.y))
                {
                    chargeDirection = 1;
                }

                else if (Mathf.RoundToInt(targetPos.y) < Mathf.RoundToInt(myPos.y))
                {
                    chargeDirection = 2;
                }
            }

            else if(Mathf.Abs(targetPos.y - myPos.y) < Mathf.Abs(targetPos.x - myPos.x))
            {
                if (Mathf.RoundToInt(targetPos.x) > Mathf.RoundToInt(myPos.x))
                {
                    chargeDirection = 3;
                }

                else if (Mathf.RoundToInt(targetPos.x) < Mathf.RoundToInt(myPos.x))
                {
                    chargeDirection = 4;
                }
            }
        }
        
        else if (holding >= chargeDuration && isHolding)
        {
            Debug.ClearDeveloperConsole();
            Debug.Log("Now it's charging");
            isCharging = true;
            isHolding = false;
        }

        if (charging < chargeDuration && isCharging)
        {
            speed = 5;
            switch (chargeDirection)
            {
                case 1:
                    rigid.velocity = new Vector2(0, speed);
                    break;
                case 2:
                    rigid.velocity = new Vector2(0, -speed);
                    break;
                case 3:
                    rigid.velocity = new Vector2(speed, 0);
                    break;
                case 4:
                    rigid.velocity = new Vector2(-speed, 0);
                    break;
            }
        }

        else if (charging >= chargeDuration)
        {
            isCharging = false;
        }
    }


}
