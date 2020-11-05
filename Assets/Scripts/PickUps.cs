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
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        //pick up item will not destrotyed by explosion right after it generated
        toBeVincible += Time.deltaTime;
        if (toBeVincible >= invincibleDuration)
        {
            isInvincible = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        //do different funtion to player object depends on its type
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
                if (PlayerController.maxBomb < 5)
                {
                    PlayerController.maxBomb += 1;
                }
                Destroy(gameObject);
            }

            if (gameObject.name == "HealthUp(Clone)")
            {
                if (PlayerController.currentHP < player.maxHP)
                {
                    PlayerController.currentHP++;
                    Destroy(gameObject);
                }
            }
        }
    }

}