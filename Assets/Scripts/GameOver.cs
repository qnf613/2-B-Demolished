using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject StageClearPanel;
    [SerializeField] private GameObject GameOverPanel;
    private void Awake()
    {
        StageClearPanel.SetActive(false);
        GameOverPanel.SetActive(false);
    }

    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            Debug.Log("GameOver!");
            GameOverPanel.SetActive(true);
        }

        if (GameObject.Find("Door") == null)
        {
            Debug.Log("StageClear!");
            StageClearPanel.SetActive(true);
        }

    }
}
