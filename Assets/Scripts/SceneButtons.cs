﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButtons : MonoBehaviour
{
    [SerializeField] private string nextSceneName;
    [SerializeField] private string CurrentScene;

    public void loadCredit()
    {
        SceneManager.LoadScene("Credits");
    }

    public void exitGame()
    {
        Application.Quit();
        Debug.Log("Exit the Game");
    }

    public void backToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void reStart()
    {
        Enemy.numberLeft = 0;//reset the number of enemy left when the player re-start the game
        SceneManager.LoadScene(CurrentScene);
    }

    public void nextStage() 
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
