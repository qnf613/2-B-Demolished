using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClear : MonoBehaviour
{
    [SerializeField] private GameObject Panel;
    private void Awake()
    {
        Panel.SetActive(false);
    }

    private void Update()
    {
        if (GameObject.Find("Door") == null)
        {
            Debug.Log("StageClear!");
            Panel.SetActive(true);
        }
    }
}
