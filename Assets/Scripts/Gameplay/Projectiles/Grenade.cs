using System.Collections;
using UnityEngine;

public class Grenade : Projectile
{

    [Header("Grenade Specific")]
    [SerializeField] private float explosionAoe = 5f;
    [SerializeField] private float arcVariance = 5f;
    [SerializeField] private GameObject onContactParticles;
    
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

    private IEnumerator ArcCoroutine() 
    {
        float t = 0;
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = target.transform.position;

        while (t < 1) 
        {
            t += Time.deltaTime * velocity;
            if (target != null) targetPosition = target.transform.position;
            Vector3 nextPosition = GetNextPosition(startPosition, targetPosition, t);
            var ownTransform = transform;
            ownTransform.forward = (nextPosition - ownTransform.position).normalized;
            ownTransform.position = nextPosition;

            yield return null;
        }

    }

    public override void StartProjectile()
    {
        StartCoroutine(ArcCoroutine());
    }

    public override void DestroyProjectile()
    {
        unitShooter.OnDie -= DestroyProjectile;
        Instantiate(onContactParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public override void SetAttributes(Unit unit, Collider target = null)
    {
        this.maskToDamage = unit.enemyMask;
        this.target = target;
        unitShooter = unit;
        velocity = unitShooter.stats.bulletSpeed;
        damage = unitShooter.stats.damage;
        transform.LookAt(target.bounds.center, Vector3.up);

        unitShooter.OnDie += DestroyProjectile;
    }

    private Vector3 GetNextPosition(Vector3 startPosition, Vector3 targetPosition, float t)
    {
        Vector3 nextPosition = Vector3.Lerp(startPosition, targetPosition, t);
        nextPosition.y += Mathf.Sin(Mathf.PI * t) * arcVariance;
        return nextPosition;
    }
    
}
