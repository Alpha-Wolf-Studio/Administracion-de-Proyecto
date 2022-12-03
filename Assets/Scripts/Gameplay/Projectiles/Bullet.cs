using System;
using System.Collections;
using UnityEngine;

public class Bullet : Projectile
{
    private Collider enemyCollider;
    
    private void OnTriggerEnter(Collider other)
    {
        if (Utils.LayerEquals(maskToDamage, other.gameObject.layer))
        {
            other.gameObject.GetComponent<Unit>().TakeDamage(damage, unitShooter.stats);
            DestroyProjectile();
        }
    }

    public override void StartProjectile()
    {
        StartCoroutine(MoveFoward());
    }

    IEnumerator MoveFoward()
    {
        while (enemyCollider != null)
        {
            transform.LookAt(enemyCollider.bounds.center, Vector3.up);
            transform.position += transform.forward * (velocity * Time.deltaTime);
            yield return null;
        }
        DestroyProjectile();
    }

    public override void SetAttributes(Unit unit, Collider target = null)
    {
        this.maskToDamage = unit.enemyMask;
        unitShooter = unit;
        velocity = unitShooter.stats.bulletSpeed;
        damage = unitShooter.stats.damage;
        enemyCollider = target;
        unitShooter.OnDie += DestroyProjectile;
    }

    public override void DestroyProjectile()
    {
        unitShooter.OnDie -= DestroyProjectile;
        Destroy(gameObject);
    }


}