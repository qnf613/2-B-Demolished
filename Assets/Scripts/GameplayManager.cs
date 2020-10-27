using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private string CurrentScene;
    private float pushTime = 0;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Application force quited");
            Application.Quit();
        }

        if (Input.GetKey(KeyCode.R))
        {
            pushTime += Time.deltaTime;
            if (pushTime >= 2f)
            {
                SceneManager.LoadScene(CurrentScene);
            }
        }
    }
}
