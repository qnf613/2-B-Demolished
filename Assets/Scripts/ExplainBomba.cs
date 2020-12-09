using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explains : MonoBehaviour
{
    [SerializeField]private GameObject text;
    private float duration = 10;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            text.SetActive(true);
            float count = 0;
            count += Time.deltaTime;
            if (count >= duration)
            {
                Destroy(gameObject);
            }
        }
    }
}
