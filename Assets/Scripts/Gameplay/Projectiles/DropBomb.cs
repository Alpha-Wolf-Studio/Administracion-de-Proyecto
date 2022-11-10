using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBomb : Projectile
{

    [Header("Drop Bomb Specific")]
    [SerializeField] private float explosionAoe = 5f;

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

    public override void SetAttributes(LayerMask maskToDamage, UnitStats stats, Collider target = null)
    {
        this.maskToDamage = maskToDamage;
        velocity = stats.bulletSpeed;
        damage = stats.damage;
    }

    public override void StartProjectile()
    {
        StartCoroutine(MoveFoward());
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
