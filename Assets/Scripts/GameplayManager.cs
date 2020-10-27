using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private string CurrentScene;
    private float pushTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
