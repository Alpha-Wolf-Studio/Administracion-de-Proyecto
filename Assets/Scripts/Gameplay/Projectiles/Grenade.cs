using System.Collections;
using UnityEngine;

public class Grenade : Projectile
{

    [Header("Grenade Specific")]
    [SerializeField] private float explosionAoe = 5f;
    [SerializeField] private float arcVariance = 5f;
    
    private Collider target;
    private UnitStats attackerStats;
    
    private void OnTriggerEnter(Collider other)
    {
        if (Utils.LayerEquals(maskToDamage, other.gameObject.layer) || other.gameObject.layer == groundLayer)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionAoe, maskToDamage);

            foreach (var collider in colliders)
            {
                collider.gameObject.GetComponent<Unit>().TakeDamage(damage, attackerStats);
            }
            Destroy(gameObject);
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
            Vector3 nextPosition = Vector3.Lerp(startPosition, targetPosition, t);
            nextPosition.y += Mathf.Sin(Mathf.PI * t) * arcVariance;
            transform.position = nextPosition;

            yield return null;
        }

    }

    public override void StartProjectile()
    {
        StartCoroutine(ArcCoroutine());
    }

    public override void SetAttributes(LayerMask maskToDamage, UnitStats stats, Collider target)
    {
        this.maskToDamage = maskToDamage;
        this.target = target;
        velocity = stats.bulletSpeed;
        damage = stats.damage;
        attackerStats = stats;
        transform.LookAt(target.bounds.center, Vector3.up);
    }

}
