using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    //componemts
    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if player contact the door with having key, stage will be clear.
        if (other.tag == "Player")
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player.hasKey)
            {
                Debug.Log("Stage Clear!");
                Destroy(gameObject);
            }
        }
    }
}
