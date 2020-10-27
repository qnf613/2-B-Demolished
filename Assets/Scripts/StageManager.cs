using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private GameObject key;

    private void Update()
    {
        if (Enemy.number <= 0)
        {
            Instantiate(key, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
