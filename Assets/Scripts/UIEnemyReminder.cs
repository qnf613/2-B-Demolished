using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEnemyReminder : MonoBehaviour
{
    Text UI;
    public void Awake()
    {
        UI = GetComponent<Text>();
    }

    public void Update()
    {
        UI.text = "Enemy Left: " + Enemy.numberLeft.ToString();
    }
}
