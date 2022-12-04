using System.Collections;
using UnityEngine;

public class DropBomb : Projectile
{

    [Header("Drop Bomb Specific")]
    [SerializeField] private float explosionAoe = 5f;
    [SerializeField] private GameObject onContactParticles;

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

    public override void SetAttributes(Unit unit, Collider target = null)
    {
        this.maskToDamage = unit.enemyMask;
        unitShooter = unit;
        velocity = unitShooter.stats.bulletSpeed;
        damage = unitShooter.stats.damage;
        unit.OnDie += DestroyProjectile;
    }

    public override void StartProjectile()
    {
        StartCoroutine(MoveFoward());
    }

    public override void DestroyProjectile()
    {
        unitShooter.OnDie -= DestroyProjectile;
        Instantiate(onContactParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    IEnumerator MoveFoward()
    {
        while (gameObject.activeSelf)
        {
            transform.position -= Vector3.up * (velocity * Time.deltaTime);
            yield return null;
        }
    }
}
