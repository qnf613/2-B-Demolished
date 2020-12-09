using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGAnimation : MonoBehaviour
{
    public GameObject[] things;
    [SerializeField]private float spawnRangeX;
    [SerializeField] private float spawnRate;
    [SerializeField] private float droppingHeight;

    private void Awake()
    {
        Invoke("SpawnThings", spawnRate);
    }

    private void SpawnThings()
    {
        int thingIndex = Random.Range(0, things.Length);
        Vector2 spawnPos = new Vector2((Random.Range(-spawnRangeX, spawnRangeX)), droppingHeight);
        Instantiate(things[thingIndex], spawnPos, things[thingIndex].transform.rotation);
        Invoke("SpawnThings", Random.Range(2.0f, 4.0f));
    }
}
