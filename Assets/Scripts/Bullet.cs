using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private LayerMask maskToDamage;
    private float damage;
    private float velocity = 5;

    private void OnTriggerEnter(Collider other)
    {
        if (Utils.LayerEquals(maskToDamage, other.gameObject.layer))
        {
            other.gameObject.GetComponent<Unit>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
    public void SetAttributes(LayerMask maskToDamage, float damage, float bulletSpeed)
    {
        if (bulletSpeed != 0)
            this.velocity = bulletSpeed;
        this.maskToDamage = maskToDamage;
        this.damage = damage;

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