using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStageAlter : MonoBehaviour
{
    AudioSource AS;
    [SerializeField] private GameObject warningPanel;
    [SerializeField] private AudioClip bgm;

    public void Awake()
    {
        //pause the game
        Time.timeScale = 0;
        AS = GetComponentInParent<AudioSource>();
        AS.playOnAwake = true;
    }

    public void Update()
    {
        if (Input.anyKeyDown)
        {
            AS.clip = bgm;
            AS.Play();
            warningPanel.SetActive(false);
            Time.timeScale = 1;
            gameObject.GetComponent<BossStageAlter>().enabled = false;
        }
    }
}
