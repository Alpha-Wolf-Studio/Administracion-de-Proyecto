using System;
using System.Linq;
using UnityEngine;

public class UnitShootBehaviour : UnitBehaviour, IShootBehaviour
{
    [SerializeField] private Transform projectileSpawn = default;
    private Projectile prefabProjectile = default;
    private Unit unit = default;

    private Collider[] enemyColliders;

    private float timeForNextShot = -1;

    public float TimeForNextShot => timeForNextShot;

    public void SetPrefabProjectile(Projectile proj) => prefabProjectile = proj;

    private void Awake()
    {
        unit = GetComponent<Unit>();
    }

    private void Update()
    {
        if (timeForNextShot > 0) timeForNextShot -= Time.deltaTime;
    }
    public override void Execute()
    {
        if(timeForNextShot < 0) 
        {
            timeForNextShot = unit.stats.fireRate;
            OnAttacking?.Invoke(true);
        }
        OnMoving?.Invoke(false);
    }

    public void SpawnProjectile() 
    {
        if(enemyColliders.Length > 0 && enemyColliders[0] != null) 
        {
            GameObject projectileGameObject = Instantiate(prefabProjectile.gameObject, projectileSpawn.position, Quaternion.identity, BulletParent.Get().GetTransform());
            Projectile projectile = projectileGameObject.GetComponent<Projectile>();
            projectile.SetAttributes(GetComponent<Unit>().enemyMask, unit.stats, enemyColliders[0]);
            projectile.StartProjectile();
        }
    }

    public override bool IsBehaviourExecutable()
    {
        enemyColliders = Physics.OverlapSphere(transform.position, unit.stats.rangeAttack + unit.stats.bonusRange, unit.enemyMask);
        //enemyColliders = enemyColliders.Where(collider => IsEnemyValid(collider)).ToArray(); // LINQ

        enemyColliders = System.Array.FindAll(enemyColliders, IsEnemyValid).ToArray(); // SYSTEM ARRAY

        if (enemyColliders.Length > 0)
        {
            return enemyColliders.Length > 0;
        }
        
        OnAttacking?.Invoke(false);
        return false;
    }

    private bool IsEnemyValid(Collider collider)
    {
        bool colliderIsNotNull = collider != null;
        if (colliderIsNotNull)
        {
            var unitComponent = collider.GetComponent<Unit>();
            bool isUnit = unitComponent != null;
            
            if (isUnit)
            {
               return AnyFlagContained(unitComponent.OwnLaneFlags, unit.AttackLaneFlags);
            }
        }
        return false;
    }

    private bool AnyFlagContained(Enum me, Enum other)
    {
        return (Convert.ToInt32(me) & Convert.ToInt32(other)) != 0;
    }

}
