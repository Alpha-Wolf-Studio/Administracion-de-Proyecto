using System;
using System.Linq;
using UnityEngine;

public class UnitShootBehaviour : UnitBehaviour
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
    public Transform GetCurrentEnemyTransform() 
    {
        if(enemyColliders.Length > 0) 
        {
            return enemyColliders[0].transform;
        }
        return null;
    } 

    public override void Execute()
    {
        if(timeForNextShot < 0) 
        {
            timeForNextShot = unit.stats.fireRate;
            OnAttacking?.Invoke(true);
        }
        if (enemyColliders[0] != null) transform.LookAt(enemyColliders[0].transform);
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
        return enemyColliders.Length > 0;
    }

    private bool IsEnemyValid(Collider collider) => collider != null && AnyFlagContained(collider.GetComponent<Unit>().OwnLaneFlags, unit.AttackLaneFlags);

    private bool AnyFlagContained(Enum me, Enum other)
    {
        return (Convert.ToInt32(me) & Convert.ToInt32(other)) != 0;
    }

}
