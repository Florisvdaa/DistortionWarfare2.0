using MoreMountains.TopDownEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public event Action<Enemy> OnEnemyDied;

    private Health health;

    private void Start()
    {
        health = GetComponent<Health>();
    }

    private void Update()
    {
        if (health.CurrentHealth <= 0)
        {
            KillEnemy();
        }
    }
    public void KillEnemy()
    {
        OnEnemyDied?.Invoke(this);
        //Destroy(gameObject);
    }
}
