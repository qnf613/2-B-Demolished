using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour
{
    public int maxHP;
    public int currentHP;
    public bool isDamaged =  false;
    [SerializeField] private float invincibleDuration = .5f;
    private float toBeVincible = 0;
    //Component
    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        currentHP = maxHP;
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //Destroy Condition
        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }

        //Don't get damaged many times with one bomb
        if (isDamaged)
        {
            toBeVincible += Time.deltaTime;
            if (toBeVincible >= invincibleDuration)
            {
                isDamaged = false;
            }
        }
    }

}
