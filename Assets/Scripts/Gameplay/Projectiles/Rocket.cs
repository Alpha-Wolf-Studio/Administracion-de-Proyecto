using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Projectile
{
    [Header("Rocket Specific")]
    [SerializeField] private float timeBeforeAiming = .5f;
    [SerializeField] private float explosionAoe = 5f;
    
    private Collider target;

    private void OnTriggerEnter(Collider other)
    {
        if (Utils.LayerEquals(maskToDamage, other.gameObject.layer) || other.gameObject.layer == groundLayer)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionAoe, maskToDamage);

            foreach (var collider in colliders)
            {
                collider.gameObject.GetComponent<Unit>().TakeDamage(damage, unitShooter.stats);
            }

            DestroyProjectile();
        }
    }

    private IEnumerator LaunchCoroutine()
    {
        float t = 0;


        while (t < timeBeforeAiming)
        {
            t += Time.deltaTime * velocity;
            Vector3 startPosition = transform.position;
            startPosition.y += t;

            transform.forward = Vector3.Normalize(startPosition - transform.position);

            transform.position = startPosition; 

            yield return null;
        }

        t = 0;
        Vector3 position = transform.position;
        transform.LookAt(target.bounds.center);
        Debug.Break();
        while (t < 1) 
        {
            t += Time.deltaTime * velocity;
            transform.position = Vector3.Lerp(position, target.bounds.center, t);
            yield return null;
        }
        transform.position = target.bounds.center;
    }

    public override void StartProjectile()
    {
        StartCoroutine(LaunchCoroutine());
    }

    public override void SetAttributes(Unit unit, Collider target)
    {
        this.maskToDamage = unit.enemyMask;
        this.target = target;
        unitShooter = unit;
        velocity = unitShooter.stats.bulletSpeed;
        damage = unitShooter.stats.damage;

        unitShooter.OnDie += DestroyProjectile;
    }

    public override void DestroyProjectile()
    {
        unitShooter.OnDie -= DestroyProjectile;
        Destroy(gameObject);
    }
}
