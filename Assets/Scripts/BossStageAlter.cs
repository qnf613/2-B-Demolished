using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStageAlter : MonoBehaviour
{
    AudioSource AS;
    [SerializeField] private GameObject warningPanel;
    [SerializeField] private AudioClip bgm;
    [SerializeField] public static bool isWarningOn;

    public void Awake()
    {
        isWarningOn = true;
        //pause the game
        Time.timeScale = 0;
        AS = GetComponentInParent<AudioSource>();
        AS.playOnAwake = true;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            AS.clip = bgm;
            AS.Play();
            warningPanel.SetActive(false);
            Time.timeScale = 1;
            gameObject.GetComponent<BossStageAlter>().enabled = false;
            isWarningOn = false;
        }

        if (ApplicationManager.isRestarted)
        {
            isWarningOn = true;
        }
    }
}
