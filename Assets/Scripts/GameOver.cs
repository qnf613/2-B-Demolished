using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject StageClearPanel;
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private GameObject door;
    private void Awake()
    {
        StageClearPanel.SetActive(false);
        GameOverPanel.SetActive(false);
        door = GameObject.Find("Door");
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            GameOverPanel.SetActive(true);
        }

        if (door.GetComponent<Door>().isOpen)
        {
            StageClearPanel.SetActive(true);
        }
    }
}
