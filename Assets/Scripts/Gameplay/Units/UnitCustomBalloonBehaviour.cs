using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCustomBalloonBehaviour : UnitBehaviour, IShootBehaviour
{
    [Header("Movement")]
    [SerializeField] private float groundOffset = 2f;
    [SerializeField] private float upDownHeight = 1f;
    [SerializeField] private float upDownSpeed = 1f;
    [SerializeField] private float limitXRight = 110f;
    [SerializeField] private float limitXLeft = -50f;
    
    [Header("Projectile")]
    [SerializeField] private Transform projectileSpawn = default;
    private Projectile prefabProjectile = default;
    private Unit unit = default;

    private float startHeight = 0;
    
    public void SetPrefabProjectile(Projectile proj) => prefabProjectile = proj;

    private void Awake()
    {
        unit = GetComponent<Unit>();
    }

    private void Start()
    {
        startHeight = transform.position.y + groundOffset;
        OnAttacking?.Invoke(true);
    }

    public void SpawnProjectile() 
    {
        GameObject projectileGameObject = Instantiate(prefabProjectile.gameObject, projectileSpawn.position, Quaternion.identity, BulletParent.Get().GetTransform());
        Projectile projectile = projectileGameObject.GetComponent<Projectile>();
        projectile.SetAttributes(GetComponent<Unit>().enemyMask, unit.stats);
        projectile.StartProjectile();
    }

    public override bool IsBehaviourExecutable() => true;

    public override void Execute()
    {
        Transform ownTransform = transform;
        
        float upDownPosition = Mathf.PingPong(Time.time * upDownSpeed, upDownHeight) - upDownHeight / 2;

        Vector3 moveDirection = new Vector3(unit.stats.velocity * Time.deltaTime * unit.signDirection, 0, 0);
        var position = ownTransform.position;
        ownTransform.LookAt(position + moveDirection);

        position += moveDirection;
        position.y = upDownPosition + startHeight;
        
        ownTransform.position = position;
        
        if(ownTransform.position.x > limitXRight || ownTransform.position.x < limitXLeft)
            Destroy(gameObject);
    }
}
