using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [Header("Projectile General")]
    [SerializeField] protected int groundLayer = 9;

    protected LayerMask maskToDamage;
    protected float damage;
    protected float velocity = 5;

    abstract public void SetAttributes(LayerMask maskToDamage, UnitStats stats, Transform target);

    abstract public void StartProjectile();  

}
