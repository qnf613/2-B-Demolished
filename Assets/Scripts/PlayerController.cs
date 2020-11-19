using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //chaging playable character related
    [SerializeField] private GameObject wBomb;
    [SerializeField] private GameObject pBomb;
    [SerializeField] private GameObject Bomba;
    [SerializeField] private GameObject Bamba;
    [SerializeField] private bool swapSwitch;
    //parameters
    public bool isDamaged = false;
    [SerializeField] private float invincibleTime;
    private float toBeVincible;
    public float speed;
    public int maxHP;
    public int defaultBombs;
    public static int currentHP;
    public static int maxBomb;
    public static int bombOnMap; //it used in PlayerController, Bomb, BombManager scripts
    //game clear condition related
    public bool hasKey;
    //component
    public Rigidbody2D rigid;
    public LayerMask levelMask;
    public SpriteRenderer sRenderer;
    //values of movement
    public float h;
    public float v;
    private bool isHorizonMove;
    private void Awake()
    {
        currentHP = maxHP;
        maxBomb = defaultBombs;
        rigid = GetComponent<Rigidbody2D>();
        hasKey = false;
        Bomba.SetActive(false);
        sRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        //Input value
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        //Check button up & down
        bool hDown = Input.GetButtonDown("Horizontal"); //horizontal pressed
        bool hUp = Input.GetButtonUp("Horizontal"); //horizontal released
        bool vDown = Input.GetButtonDown("Vertical"); //vertical pressed
        bool vUp = Input.GetButtonUp("Vertical"); //vertical released
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
        //Swap the bomba & bamba
        if (Input.GetKeyDown(KeyCode.E) && bombOnMap == 0)
        {
            if (swapSwitch)
            {
                Bomba.SetActive(false);
                Bamba.SetActive(true);
                swapSwitch = false;
            }
            else
            {
                Bamba.SetActive(false);
                Bomba.SetActive(true);
                swapSwitch = true;
            }
            
        }
        //Bomb plant, if player reached maximum number of the bomb it can plant, player can not plant the bomb
        if (Input.GetKeyDown(KeyCode.Space) && -1 < bombOnMap && bombOnMap < maxBomb)
        {
            CreateBombs();
        }
        //sometimes number of the bomb get lower than 0, it will fix that
        if (bombOnMap <= 0)
        {
            bombOnMap = 0;
        }
        //Damage taken condition & invincible
        if (isDamaged)
        {
            sRenderer.color = new Color(1, 1, 1, .4f);
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
            sRenderer.color = new Color(1, 1, 1, 1);
        }
        //Death condition
        if (currentHP <= 0)
        {
            SoundManager.instance.PlayGameOver();
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
            if (swapSwitch)
            {
                Instantiate(pBomb, new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y)), transform.rotation);
            }
            else
            {
                Instantiate(wBomb, new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y)), transform.rotation);
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //when player contact with enemy, player get damage
        if (other.gameObject.tag == "Enemies" && !isDamaged)
        {
            currentHP--;
            SoundManager.instance.PlayDamaged();
            isDamaged = true;
        }
    }
}
