using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject bomb;
    public bool isDamaged = false;
    [SerializeField] private int maxHP;
    public int currentHP;
    //component
    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    //values of movement
    private float h;
    private float v;
    private bool isHorizonMove;
    
    private void Awake()
    {
        currentHP = maxHP;
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //Input value
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        //Check button up & down
        bool hDown = Input.GetButton("Horizontal");
        bool hUp = Input.GetButtonUp("Horizontal");
        bool vDown = Input.GetButton("Vertical");
        bool vUp = Input.GetButtonUp("Vertical");
        //Cross direction movement
        if (hDown || vUp)
        {
            isHorizonMove = true;
        }
        else if (vDown || hUp)
        {
            isHorizonMove = false;
        }
        //Bomb plant
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bomb, new Vector2 (Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y)), transform.rotation);
            
        }
        //DeathCondition
        if (currentHP == 0)
        {
            Destroy(gameObject);
        }

    }

    private void FixedUpdate()
    {
        //Movement
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
        rigid.velocity = moveVec * speed;
    }
}
