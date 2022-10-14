using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyType enemyType = default;

    private Unit unit = default;
    
    private void Start()
    {
        unit = GetComponent<Unit>();
    }

    public EnemyConfigurations GetCurrentConfiguration()
    {
        EnemyConfigurations config = new EnemyConfigurations();
        config.EnemyPosition = transform.position;
        config.EnemyRotation = transform.eulerAngles;
        config.TypeOfEnemy = enemyType;
        config.AttackLaneFlags = unit.AttackLaneFlags;
        config.OwnLaneFlags = unit.OwnLaneFlags;
        return config;
    }
    
}
