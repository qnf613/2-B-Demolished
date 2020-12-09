using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    //this script will control re-start and force quit the game
    [SerializeField] private string CurrentScene;
    private float pushTime = 0;
    //restart
    public static bool isRestarted = false;

    private void Awake()
    {
        if (pushTime != 0)
        {
            pushTime = 0;
        }
    }

    private void Update()
    {
        //if player press the esc, game will be set down   
        if (Input.GetKey(KeyCode.Escape))
        {
            pushTime += Time.deltaTime;
            if (pushTime >= 2f)
            {
                Application.Quit();
            }
        }
        //if player press the R for 2 seconds, stage will be re-start
        if (Input.GetKey(KeyCode.R))
        {
            pushTime += Time.deltaTime;
            if (pushTime >= 2f)
            {
                Enemy.numberLeft = 0;//reset the number of enemy left when the player re-start the game
                isRestarted = true;
                SceneManager.LoadScene(CurrentScene);
            }
        }
    }
}
