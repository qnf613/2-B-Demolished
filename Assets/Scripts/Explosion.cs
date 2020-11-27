using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public int exDamage;
    public bool bombTypeP;

    private void Update()
    {
        Destroy(gameObject, .3f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //do different damage to other game objects depend on their tag
        if (other.tag == "Player")
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (!player.isDamaged)
            {
                PlayerController.currentHP--;
                player.isDamaged = true;
            }
        }

        if (other.tag == "SoftBlock")
        {
            Blocks block = other.GetComponent<Blocks>();
            if (!block.isDamaged)
            {
                block.currentHP -= exDamage;
            }
        }

        if (other.tag == "HardBlock")
        {
            Blocks block = other.GetComponent<Blocks>();
            if (!block.isDamaged && bombTypeP)
            {
                block.currentHP -= exDamage;
            }
            else if (!block.isDamaged && !bombTypeP)
            {
                block.currentHP -= 0;
            }
        }

        if (this.tag == "Explosion")
        {
            if (other.tag == "EnemyDamage")
            {
                Enemy enemy = other.GetComponentInParent<Enemy>();
                if (!enemy.isDamaged)
                {
                    enemy.currentHP -= exDamage;
                    enemy.isDamaged = true;
                }
            }
        }
        
        if (other.tag == "PickUps")
        {
            PickUps pickup = other.GetComponent<PickUps>();
            if (!pickup.isInvincible)
            {
                Destroy(other.gameObject);
            }
        }
    }


}
