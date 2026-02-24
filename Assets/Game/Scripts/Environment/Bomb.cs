using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float maxRadius = 5f;
    [SerializeField] private float expansionSpeed = 5f;
    [SerializeField] private float damage = 20;
    [SerializeField] private LayerMask targetMask;

    private bool explode = false;

    private float currentRadius = 0f;
    private bool hasDealtDamage = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet"))
            explode = true;
    }

    private void Update()
    {

        if(!explode) return;

        // Expand Radius
        currentRadius += expansionSpeed * Time.deltaTime;

        // visualise in scene 
        Debug.DrawLine(transform.position, transform.position + Vector3.right * currentRadius, Color.cyan);

        // When radius reaches max, apply damage once
        if(!hasDealtDamage && currentRadius >= maxRadius)
        {
            DealDamage();
            hasDealtDamage = true;
            explode = false;
            Destroy(gameObject, 0.1f);
        }
    }

    private void DealDamage()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, maxRadius, targetMask);

        foreach (Collider2D hit in hits)
        {
            var health = hit.GetComponent<Health>();
            if (health != null)
                health.Damage(damage, this.gameObject, 0.4f, 0.5f, new Vector3(0, 0, 0));
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, currentRadius);
    }
}
