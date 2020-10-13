using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    [SerializeField] private float countdown = 3f;
    [SerializeField] private int damage;
    private float toBeObject = 0;
    //Component
    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

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
        GetComponent<SpriteRenderer>().enabled = false;
        Destroy(gameObject, .5f);

    }


}
