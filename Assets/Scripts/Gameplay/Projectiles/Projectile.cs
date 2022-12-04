using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    
    [Header("Projectile General")]
    [SerializeField] protected int groundLayer = 9;

    protected Unit unitShooter;
    protected LayerMask maskToDamage;
    protected float damage;
    protected float velocity = 5;

    public abstract void SetAttributes(Unit shooter, Collider target = null);
    public abstract void StartProjectile();
    public abstract void DestroyProjectile();

}
