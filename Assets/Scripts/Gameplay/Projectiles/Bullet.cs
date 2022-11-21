using System.Collections;
using UnityEngine;

public class Bullet : Projectile
{

    private UnitStats attackerStats;
    private Collider enemyCollider;
    
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
        while (enemyCollider != null)
        {
            transform.LookAt(enemyCollider.bounds.center, Vector3.up);
            transform.position += transform.forward * (velocity * Time.deltaTime);
            yield return null;
        }
        Destroy(gameObject);
    }

    public override void SetAttributes(LayerMask maskToDamage, UnitStats stats, Collider target)
    {
        this.maskToDamage = maskToDamage;
        attackerStats = stats;
        velocity = stats.bulletSpeed;
        damage = stats.damage;
        enemyCollider = target;
    }

}