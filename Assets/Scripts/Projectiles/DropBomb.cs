using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBomb : Projectile
{

    [Header("Drop Bomb Specific")]
    [SerializeField] private float explosionAoe = 5f;
    private Collider target;

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

    public override void SetAttributes(LayerMask maskToDamage, UnitStats stats, Collider target)
    {
        this.maskToDamage = maskToDamage;
        this.target = target;
        velocity = stats.bulletSpeed;
        damage = stats.damage;
        transform.LookAt(target.bounds.center, Vector3.up);
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

}
