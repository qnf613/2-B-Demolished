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
    public int currentHP;
    [SerializeField] private int speed;
    private int hSpeed;
    private int vSpeed;
    //movement direction related
    [SerializeField] private int nextMove;
    //target player related
    [SerializeField] private bool isTargeting;
    //enemy type variation
    [SerializeField] private bool chaser = false;
    [SerializeField] private bool charger = false;
    [SerializeField] private float chargeDuration = 2f;
    private int originalSpeed;
    private float holding = 0;
    private float charging = 0;
    [SerializeField] private bool isHolding = false;
    [SerializeField] private bool isCharging = false;
    private int chargeDirection; //1 = up, 2 = down, 3 = right, 4 = left
    [SerializeField] private bool bomber = false;
    //check up how many enemies on the stage
    public static int numberLeft;

    private void Awake()
    {
        currentHP = maxHP;
        //hSpeed & vSpeed inheritance speed's value to be used in animator
        hSpeed = speed;
        vSpeed = speed;
        //keep original speed for 'charger' to back to normal speed from charge speed
        originalSpeed = speed;
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anima = GetComponent<Animator>();
        StartCoroutine("ChangeMovement");
        anima.SetBool("isCharge", false);
        if (gameObject.activeSelf == true)
        {
            numberLeft++;
        }
    }

    private void Update()
    {
        //death condition
        if (currentHP <= 0)
        {
            numberLeft--;
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
            speed = originalSpeed;
            charging = 0;
            anima.SetBool("isCharge", false);
        }
        else if (isCharging)
        {
            charging += Time.deltaTime;
            anima.SetBool("isCharge", true);
        }

        //animation
        if (anima.GetInteger("hMove") != hSpeed)
        {
            anima.SetBool("isChange", true);
            anima.SetInteger("hMove", hSpeed);
        }
        else if (anima.GetInteger("vMove") != vSpeed)
        {
            anima.SetBool("isChange", true);
            anima.SetInteger("vMove", vSpeed);
        }
        else
        {
            anima.SetBool("isChange", false);
        }
        //reset the number of enemy left when the player re-start the game
        if (ApplicationManager.isRestarted)
        {
            numberLeft = 0;
            ApplicationManager.isRestarted = false;
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
        //movement direction && animation
        switch (nextMove)
        {
            case 0:
                vSpeed = 0;
                hSpeed = 0;
                rigid.velocity = new Vector2(vSpeed, hSpeed);
                break;
            case 1:
                hSpeed = 0;
                vSpeed = speed;
                rigid.velocity = new Vector2(hSpeed, vSpeed);
                break;
            case 2:
                hSpeed = 0;
                vSpeed = -speed;
                rigid.velocity = new Vector2(hSpeed, vSpeed);
                break;
            case 3:
                hSpeed = speed;
                vSpeed = 0;
                rigid.velocity = new Vector2(hSpeed, vSpeed);
                break;
            case 4:
                hSpeed = -speed;
                vSpeed = 0;
                rigid.velocity = new Vector2(hSpeed, vSpeed);
                break;
        }
        
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
                    hSpeed = 0;
                    vSpeed = speed;
                    rigid.velocity = new Vector2(hSpeed, vSpeed);
                    break;
                case 2:
                    hSpeed = 0; 
                    vSpeed = -speed;
                    rigid.velocity = new Vector2(hSpeed, vSpeed);
                    break;
                case 3:
                    hSpeed = speed;
                    vSpeed = 0;
                    rigid.velocity = new Vector2(hSpeed, vSpeed);
                    break;
                case 4:
                    hSpeed = -speed;
                    vSpeed = 0;
                    rigid.velocity = new Vector2(hSpeed, vSpeed);
                    break;
            }
        }

        else if (charging >= chargeDuration)
        {
            isCharging = false;
            anima.SetBool("isCharge", false);
        }
    }


}
