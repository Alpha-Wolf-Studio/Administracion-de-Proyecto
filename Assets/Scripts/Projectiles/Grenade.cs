using System.Collections;
using UnityEngine;

public class Grenade : Projectile
{

    [Header("Grenade Specific")]
    [SerializeField] private float explosionAoe = 5f;
    [SerializeField] private float arcVariance = 5f;
    private Transform target;

    private void OnTriggerEnter(Collider other)
    {
        if (Utils.LayerEquals(maskToDamage, other.gameObject.layer) || other.gameObject.layer == groundLayer)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionAoe, maskToDamage);

            foreach (var collider in colliders)
            {
                collider.gameObject.GetComponent<Unit>().TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }

    private IEnumerator ArcCoroutine() 
    {
        float t = 0;
        Vector3 startPosition = transform.position;
        

        while(t < 1) 
        {
            t += Time.deltaTime * velocity;
            Vector3 nextPosition = Vector3.Lerp(startPosition, target.position, t);
            nextPosition.y += Mathf.Sin(Mathf.PI * t) * arcVariance;
            transform.position = nextPosition;

            yield return null;
        }

    }

    public override void StartProjectile()
    {
        StartCoroutine(ArcCoroutine());
    }

    public override void SetAttributes(LayerMask maskToDamage, UnitStats stats, Transform target)
    {
        this.maskToDamage = maskToDamage;
        this.target = target;
        velocity = stats.bulletSpeed;
        damage = stats.damage;
        transform.LookAt(target, Vector3.up);
    }

}
