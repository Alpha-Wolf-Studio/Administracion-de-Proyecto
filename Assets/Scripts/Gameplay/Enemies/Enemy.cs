using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyType enemyType = default;
    [SerializeField] private Renderer[] renderers;
    [SerializeField] private Material[] materials;
    public Unit Unit { get; private set; } = default;

    public int EnemyIndex { get; set; }
    
    private void Start()
    {
        Unit = GetComponent<Unit>();
    }

    public EnemyConfigurations GetCurrentConfiguration()
    {
        EnemyConfigurations config = new EnemyConfigurations();
        config.EnemyIndex = EnemyIndex;
        config.EnemyPosition = transform.position;
        config.EnemyRotation = transform.eulerAngles;
        config.TypeOfEnemy = enemyType;
        config.AttackLaneFlags = Unit.AttackLaneFlags;
        config.OwnLaneFlags = Unit.OwnLaneFlags;
        return config;
    }

    public void SetRenderers(int terrainIndex)
    {
        foreach (var rend in renderers)
        {
            rend.material = materials[terrainIndex];
        }
    }
    
}
