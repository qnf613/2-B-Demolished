using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    [SerializeField] private float countdown = 3f;
    [SerializeField] private int damage;
    private float toBeNormalBomb = 0;
    [SerializeField] private bool isBombTypeP;
    //Component
    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public LayerMask levelMask;
    private void Start()
    {
        gameObject.layer = 16;
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        Invoke("Blowup", countdown);
    }

    private void Update()
    {
        Explosion ex = explosion.GetComponent<Explosion>();
        ex.exDamage = damage;
        if (isBombTypeP)
        {
            ex.bombTypeP = true;
        }
        else
        {
            ex.bombTypeP = false;
        }

        toBeNormalBomb += Time.deltaTime;

        if (toBeNormalBomb >= 1f)
        {
            gameObject.tag = "PlayerBomb";
        }

        if (!gameObject.CompareTag("BombJustPlanted"))
        {
            gameObject.layer = 15;
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
        PlayerController.bombOnMap--;
        Destroy(gameObject, .1f);

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Explosion")
        {
            Invoke("Blowup", .1f);
        }
    }
}

