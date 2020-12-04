using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBossHP : MonoBehaviour
{
    Text UI;
    public void Awake()
    {
        UI = GetComponent<Text>();
    }

    public void Update()
    {
        UI.text = "Boss HP: " + Enemy.bossHp.ToString();
    }
}
