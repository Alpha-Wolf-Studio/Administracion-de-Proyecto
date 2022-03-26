using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage;
    private LayerMask maskToDamage;
    private float velocity = 5;

    private void OnTriggerEnter(Collider other)
    {
        if (Utils.LayerEquals(maskToDamage, other.gameObject.layer))
        {
            other.gameObject.GetComponent<Unit>().RecibeDamage(damage);
            Destroy(gameObject);
        }
    }
    public void SetAttributes(LayerMask maskToDamage, int damage)
    {
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
