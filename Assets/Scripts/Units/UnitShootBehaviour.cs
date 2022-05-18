using UnityEngine;

public class UnitShootBehaviour : UnitBehaviour
{
    [SerializeField] private Projectile prefabProjectile = default;
    [SerializeField] private Transform projectileSpawn = default;
    private Unit unit = default;

    private Collider[] enemyColliders = new Collider[5]; //Puse 5 para que sobre en caso de una nueva utilidad ya que estoy reutilizando el array, con tener 1 ya alcanza.
    private int currentAmountOfEnemies = 0;

    private float timeForNextShot = -1;

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
        if(currentAmountOfEnemies > 0) 
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
            OnAttacked?.Invoke();
        }
        if (enemyColliders[0] != null) transform.LookAt(enemyColliders[0].transform);
        OnMoved?.Invoke(false);
    }

    public void SpawnProjectile() 
    {
        if(enemyColliders[0] != null) 
        {
            GameObject projectileGameObject = Instantiate(prefabProjectile.gameObject, projectileSpawn.position, Quaternion.identity, BulletParent.Get().GetTransform());
            Projectile projectile = projectileGameObject.GetComponent<Projectile>();
            projectile.SetAttributes(GetComponent<Unit>().enemyMask, unit.stats, enemyColliders[0].transform);
            projectile.StartProjectile();
        }
    }

    public override bool IsBehaviourExecutable()
    {
        currentAmountOfEnemies = Physics.OverlapSphereNonAlloc(transform.position, unit.stats.rangeAttack + unit.stats.bonusRange, enemyColliders, unit.enemyMask);
        return currentAmountOfEnemies > 0;
    }
}
