using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitReaper : MonoBehaviour
{
    private Health health;
    [SerializeField] private SpriteRenderer sprite;

    [Header("Flicker settings")]
    [SerializeField] private float invulnerableDuration = 1f;
    [SerializeField] private float vulnerableDuration = 1f;
    [SerializeField] private float invulnerableAlpha = 0.3f;

    private void Awake()
    {
        health = GetComponent<Health>();
        //sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        StartCoroutine(Flicker());
    }

    private IEnumerator Flicker()
    {
        while (health.CurrentHealth > 0)
        {
            // INVULNERABLE
            health.Invulnerable = true;
            SetAlpha(invulnerableAlpha);
            yield return new WaitForSeconds(invulnerableDuration);

            // VULNERABLE
            health.Invulnerable = false;
            SetAlpha(1f);
            yield return new WaitForSeconds(vulnerableDuration);
        }
    }
    private void SetAlpha(float a)
    {
        if (sprite == null) return;

        Color c = sprite.color;
        c.a = a;
        sprite.color = c;
    }

}
