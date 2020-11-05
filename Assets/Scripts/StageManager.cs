using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{

    [SerializeField] private GameObject key;
    
    private void Update()
    {
        //this script check up the clear conditions and generate the key
        if (Enemy.numberLeft <= 0)
        {
            Instantiate(key, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
