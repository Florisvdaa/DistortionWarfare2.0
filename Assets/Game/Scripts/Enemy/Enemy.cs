using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public event Action<Enemy> OnEnemyDied;

    private Health health;
    private MMHealthBar healthBar;

    private void Start()
    {
        health = GetComponent<Health>();
        healthBar = GetComponent<MMHealthBar>();
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

        Invoke("RemoveEnemy", 3f);
    }

    private void RemoveEnemy()
    {
        Destroy(gameObject);
        //Destroy(healthBar.gameObject);
    }
}
