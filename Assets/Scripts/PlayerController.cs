using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject bomb;
    public bool isDamaged = false;
    [SerializeField] private float invincibleTime;
    private float toBeVincible;
    public float speed;
    public int maxHP;
    public int currentHP;
    public int maxBomb;
    public static int bombOnMap; //it used in PlayerController, Bomb, BombManager scripts
    [SerializeField] private int tempMaxBombViwer;
    //component
    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public LayerMask levelMask;
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
        tempMaxBombViwer = bombOnMap;
        //Input value
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        //Check button up & down
        bool hDown = Input.GetButtonDown("Horizontal");
        bool hUp = Input.GetButtonUp("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool vUp = Input.GetButtonUp("Vertical");
        //Cross direction movement
        if (hDown)
        {
            isHorizonMove = true;
        }
        else if (vDown)
        {
            isHorizonMove = false;
        }
        else if (hUp || vUp)
        {
            isHorizonMove = h != 0;
        }

        //Animation
        if (animator.GetInteger("hAxisRaw") != h)
        {
            animator.SetBool("isChange", true);
            animator.SetInteger("hAxisRaw", (int)h);
        }
        else if (animator.GetInteger("vAxisRaw") != v)
        {
            animator.SetBool("isChange", true);
            animator.SetInteger("vAxisRaw", (int)v);
        }
        else
        {
            animator.SetBool("isChange", false);
        }
        //Bomb plant
        if (Input.GetKeyDown(KeyCode.Space) && -1 < bombOnMap && bombOnMap < maxBomb)
        {
            CreateBombs();
        }
        if (bombOnMap <= 0)
        {
            bombOnMap = 0;
        }
        //Damage taken condition & invincible
        if (isDamaged)
        {
            gameObject.layer = 10;
            toBeVincible += Time.deltaTime;
            if (toBeVincible >= invincibleTime)
            {
                isDamaged = false;
            }
        }
        else if (!isDamaged)
        {
            gameObject.layer = 9;
            toBeVincible = 0;
        }
        //Death condition
        if (currentHP <= 0)
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

    private void CreateBombs()
    {
        //limits one bomb plant per one tile
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.position, .1f, levelMask);
        if (!hit.collider)
        {
            bombOnMap++;
            Instantiate(bomb, new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y)), transform.rotation);
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemies" && !isDamaged)
        {
            currentHP--;
            isDamaged = true;
        }
    }
}
