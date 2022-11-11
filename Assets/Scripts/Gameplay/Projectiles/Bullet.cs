using System.Collections;
using UnityEngine;

public class Bullet : Projectile
{

    private UnitStats attackerStats;
    
    private void OnTriggerEnter(Collider other)
    {
        if (Utils.LayerEquals(maskToDamage, other.gameObject.layer))
        {
            other.gameObject.GetComponent<Unit>().TakeDamage(damage, attackerStats);
            Destroy(gameObject);
        }
    }

    public override void StartProjectile() 
    {
        StartCoroutine(MoveFoward());
    }

    IEnumerator MoveFoward()
    {
        while (true)
        {
            transform.position += transform.forward * (velocity * Time.deltaTime);
            yield return null;
        }
    }

    public override void SetAttributes(LayerMask maskToDamage, UnitStats stats, Collider target)
    {
        this.maskToDamage = maskToDamage;
        attackerStats = stats;
        velocity = stats.bulletSpeed;
        damage = stats.damage;
        transform.LookAt(target.bounds.center, Vector3.up);
    }

}