using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] public int damage;

    private void Start()
    {
        
    }


    private void Update()
    {
        Destroy(gameObject, .5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //do different damage to other game objects depend on their tag
        if (other.tag == "Player")
        {
            PlayerController player = other.GetComponent<PlayerController>();
            player.currentHP--;
        }

        if (other.tag == "SoftBlock")
        {
            Blocks block = other.GetComponent<Blocks>();
            if (!block.isDamaged)
            {
                block.isDamaged = true;
                block.currentHP -= damage;
            }
        }

        if (other.tag == "HardBlock")
        {
            Blocks block = other.GetComponent<Blocks>();
            if (!block.isDamaged && damage >= 3)
            {
                block.isDamaged = true;
                block.currentHP -= damage;
            }
        }

        if (other.tag == "damageTrigger")
        {
            EnemyDamageTrigger enemy = other.GetComponent<EnemyDamageTrigger>();
            enemy.enemyHP -= damage;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
    }
}
