using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject Panel;
    private void Awake()
    {
        Panel.SetActive(false);
    }

    private void Update()
    {
        if (GameObject.Find("Player") == null)
        {
            Debug.Log("GameOver!");
            Panel.SetActive(true);
        }
    }
}
