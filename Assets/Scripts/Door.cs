using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] private string nextSceneName;
    //componemts
    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player.hasKey)
            {
                Debug.Log("Stage Clear!");
                SceneManager.LoadScene(nextSceneName);
            }
        }
    }
}
