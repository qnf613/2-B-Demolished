using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageTrigger : MonoBehaviour
{
    public int enemyHP;

    private void Start()
    {
        enemyHP = Enemy.currentHP;
    }

    private void Update()
    {
        Enemy.currentHP = enemyHP;   
    }
}
