using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUps : MonoBehaviour
{
    [SerializeField] private float invincibleDuration = 1f;
    private float toBeVincible = 0;
    public bool isInvincible;
    //component
    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private void Awake()
    {
        isInvincible = true;
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        toBeVincible += Time.deltaTime;
        if (toBeVincible >= invincibleDuration)
        {
            isInvincible = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        //do different damage to other game objects depend on their tag
        if (other.tag == "Player")
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (gameObject.name == "SpeedUp(Clone)")
            {
                if (player.speed < 7)
                {
                    player.speed += .8f;
                }
                Destroy(gameObject);
            }
            
            if (gameObject.name == "ExtraBomb(Clone)")
            {
                if (player.maxBomb < 5)
                {
                    player.maxBomb += 1;
                }
                Destroy(gameObject);
            }

            if (gameObject.name == "HealthUp(Clone)")
            {
                if (player.currentHP < player.maxHP)
                {
                    player.currentHP++;
                    Destroy(gameObject);
                }
            }
        }
    }

}