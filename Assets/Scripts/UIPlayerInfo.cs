using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerInfo : MonoBehaviour
{
    Text UI;
    public void Awake()
    {
        UI = GetComponent<Text>();
    }

    public void Update()
    {
        PlayerController playerInfo = GetComponent<PlayerController>();

        UI.text = "HP: " + PlayerController.currentHP + " | Max Bomb: " + PlayerController.maxBomb + "\nBomb On The Map: " + PlayerController.bombOnMap;
    }
}
