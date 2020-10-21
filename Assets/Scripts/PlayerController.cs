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
    public static int bombOnMap; //it used in both PlayerController & Bomb script
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
        //Input value
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        //Check button up & down
        bool hDown = Input.GetButtonDown("Horizontal");
        bool hUp = Input.GetButtonUp("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
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
        if (Input.GetKeyDown(KeyCode.Space) && bombOnMap < maxBomb)
        {
            bombOnMap++;
            CreateBombs();
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
