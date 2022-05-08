using System.Collections;
using UnityEngine;

public class Bullet : Projectile
{

    private void OnTriggerEnter(Collider other)
    {
        if (Utils.LayerEquals(maskToDamage, other.gameObject.layer))
        {
            other.gameObject.GetComponent<Unit>().TakeDamage(damage);
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

    public override void SetAttributes(LayerMask maskToDamage, UnitStats stats, Transform target)
    {
        this.maskToDamage = maskToDamage;
        velocity = stats.bulletSpeed;
        damage = stats.damage;
        transform.LookAt(target, Vector3.up);
    }

}