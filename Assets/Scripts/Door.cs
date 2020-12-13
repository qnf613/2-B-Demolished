using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    //componemts
    private Animator anima;
    private BoxCollider2D boxcol;

    private bool isIndicating;
    public bool isOpen;
    private float flashingRepetition = .5f;
    private float doorOpeningPosition = .43f;
    private float doorOpeningWidth = 1.88f;
    private float waitForDoorOpen = .5f;
    private float timeCounter = 0;

    private void Awake()
    {
        isIndicating = false;
        isOpen = false;
        anima = GetComponent<Animator>();
        boxcol = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (isOpen)
        {
            //waiting for door open before puase the game
            timeCounter += Time.deltaTime;
            if (timeCounter >= waitForDoorOpen)
            {
                Time.timeScale = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if player contact the door with having key, stage will be clear.
        if (other.tag == "Player")
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player.hasKey)
            {
                SoundManager.instance.PlayStageClear();
                //changes to the door
                isOpen = true;
                anima.SetBool("isOpened", true);
                this.transform.position = transform.position + new Vector3(doorOpeningPosition, 0,0);
                boxcol.size = new Vector2(doorOpeningWidth, 0);
                player.hasKey = false; //make player can not open multiple times
            }
            else if (!player.hasKey && Enemy.numberLeft != 0 && !isIndicating)
            {
                isIndicating = true;
                StartCoroutine("notification");
            }
        }
    }

    private IEnumerator notification()
    {
        UIEnemyReminder.UI.color = new Color(1, 1, 0);
        yield return new WaitForSecondsRealtime(flashingRepetition);
        UIEnemyReminder.UI.color = new Color(1, 1, 1);
        yield return new WaitForSecondsRealtime(flashingRepetition);
        UIEnemyReminder.UI.color = new Color(1, 1, 0);
        yield return new WaitForSecondsRealtime(flashingRepetition);
        UIEnemyReminder.UI.color = new Color(1, 1, 1);
        yield return new WaitForSecondsRealtime(flashingRepetition);
        UIEnemyReminder.UI.color = new Color(1, 1, 0);
        yield return new WaitForSecondsRealtime(flashingRepetition);
        UIEnemyReminder.UI.color = new Color(1, 1, 1);
        isIndicating = false;
    }


}
