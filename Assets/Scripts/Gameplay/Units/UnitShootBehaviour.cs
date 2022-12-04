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
    private bool firstAttack = false;
    public bool CheckReAttack { get; set; } = true;
    public Action OnReAttack { get; set; }
    
    public float TimeForNextShot => timeForNextShot;

    public void SetPrefabProjectile(Projectile proj) => prefabProjectile = proj;

    private void Awake()
    {
        unit = GetComponent<Unit>();
    }

    public override void Execute()
    {
        if (firstAttack)
        {
            firstAttack = false;
            OnAttacking?.Invoke(true);
            timeForNextShot = 1 / unit.stats.fireRate;
        }
        else if (CheckReAttack)
        {
            if (timeForNextShot > 0)
            {
                timeForNextShot -= Time.deltaTime;
            }
            else
            {
                timeForNextShot = 1 / unit.stats.fireRate;
                OnReAttack?.Invoke();
                CheckReAttack = false;
            }
        }
        
        Vector3 enemyPosition = enemyColliders[0].transform.position;
        Vector3 ownPosition = transform.position;

        enemyPosition.y = ownPosition.y;

        transform.forward = (enemyPosition - ownPosition).normalized;

        OnMoving?.Invoke(false);
    }

    public void SpawnProjectile() 
    {
        if(enemyColliders.Length > 0 && enemyColliders[0] != null) 
        {
            GameObject projectileGameObject = Instantiate(prefabProjectile.gameObject, projectileSpawn.position, Quaternion.identity, BulletParent.Get().GetTransform());
            Projectile projectile = projectileGameObject.GetComponent<Projectile>();
            projectile.SetAttributes(unit, enemyColliders[0]);
            projectile.StartProjectile();
        }
    }

    public override bool IsBehaviourExecutable()
    {
        enemyColliders = Physics.OverlapSphere(transform.position, unit.stats.rangeAttack + unit.stats.bonusRange, unit.enemyMask);
        //enemyColliders = enemyColliders.Where(collider => IsEnemyValid(collider)).ToArray(); // LINQ

        enemyColliders = System.Array.FindAll(enemyColliders, IsEnemyValid).ToArray(); // SYSTEM ARRAY

        bool enemyInSight = enemyColliders.Length > 0;

        if (!enemyInSight)
        {
            CheckReAttack = false;
            firstAttack = true;
        }
        
        OnAttacking?.Invoke(enemyInSight);
        return enemyInSight;
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
