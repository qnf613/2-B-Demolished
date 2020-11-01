using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour
{
    //prefabs of pick ups
    [SerializeField] private GameObject speedUp;
    [SerializeField] private GameObject maxBombUp;
    [SerializeField] private GameObject hpUp;
    //numbering for pick up
    public int pickupNum;
    //chance to get pick up item
    [SerializeField] [Tooltip("possibility of pickup items appear (%)")] private float pickupChance;
    //generate one pick up item per block
    [SerializeField] private int generateOnce;
    public int maxHP;
    public int currentHP;
    //make block get damage once per .5 sec
    public bool isDamaged = false;
    [SerializeField] private float invincibleDuration = 1f;
    private float toBeVincible = 0;
    //Component
    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        currentHP = maxHP;
        generateOnce = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //Destroy Condition
        if (currentHP <= 0 && generateOnce < 1)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GeneratePickUp();
            Destroy(gameObject, .1f);
        }

        //Don't get damaged many times with one bomb
        if (isDamaged)
        {
            toBeVincible += Time.deltaTime;
            if (toBeVincible >= invincibleDuration)
            {
                isDamaged = false;
            }
        }
    }

    private void GeneratePickUp()
    {
        //pop up the random pick up item randomly when blocks are destroyed 
        if (Random.Range(1, 101) <= pickupChance)
        {
            pickupNum = Random.Range(0, 3);
            switch (pickupNum)
            {
                case 0:
                    Instantiate(speedUp, transform.position, transform.rotation);
                    generateOnce++;
                    break;
                case 1:
                    Instantiate(maxBombUp, transform.position, transform.rotation);
                    generateOnce++;
                    break;
                case 2:
                    Instantiate(hpUp, transform.position, transform.rotation);
                    generateOnce++;
                    break;
            }
        }
    }

}
