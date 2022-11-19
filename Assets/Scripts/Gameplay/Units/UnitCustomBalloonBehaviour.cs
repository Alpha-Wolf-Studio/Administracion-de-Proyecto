using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCustomBalloonBehaviour : UnitBehaviour, IShootBehaviour
{
    public Action OnReAttack { get; set; }
    public bool CheckReAttack { get; set; } = false;
    
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

    private float timeForNextShot = -1;
    
    private float startHeight = 0;

    private const float TopZPosition = 10f;
    private const float MidZPosition = 0f;
    private const float BotZPosition = -10f;

    public void SetPrefabProjectile(Projectile proj)
    {
        prefabProjectile = proj;
        OnAttacking?.Invoke(true);
    } 

    private void Awake()
    {
        unit = GetComponent<Unit>();
    }

    private void Start()
    {
        startHeight = transform.position.y + groundOffset;
        Vector3 startPosition = new Vector3(limitXRight, startHeight, 0);

        if (unit.OwnLaneFlags.HasFlag(LanesFlags.Bottom))
            startPosition.z = BotZPosition;
        else if (unit.OwnLaneFlags.HasFlag(LanesFlags.Mid))
            startPosition.z = MidZPosition;
        else //if (unit.OwnLaneFlags.HasFlag(LanesFlags.Top))
            startPosition.z = TopZPosition;

        transform.position = startPosition;
    }

    private void Update()
    {
        if (!CheckReAttack) return;
        if (timeForNextShot > 0) timeForNextShot -= Time.deltaTime;
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
        if (timeForNextShot < 0)
        {
            if (CheckReAttack)
            {
                OnReAttack?.Invoke();
                CheckReAttack = false;
            }
            timeForNextShot = 1 / unit.stats.fireRate;
            OnAttacking?.Invoke(true);
        }
        
        Transform ownTransform = transform;
        
        float upDownPosition = Mathf.PingPong(Time.time * upDownSpeed, upDownHeight) - upDownHeight / 2;

        Vector3 moveDirection = new Vector3(unit.stats.velocity * Time.deltaTime * unit.signDirection, 0, 0);
        var position = ownTransform.position;
        ownTransform.LookAt(position + moveDirection);

        position += moveDirection;
        position.y = upDownPosition + startHeight;
        
        ownTransform.position = position;

        if (ownTransform.position.x < limitXLeft)
        {
            Vector3 newPosition = ownTransform.position;
            newPosition.x = limitXRight;
            ownTransform.position = newPosition;
        }
    }
}
