using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplainBomba : MonoBehaviour
{
    [SerializeField]private GameObject text;
    private float duration = 10;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
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
