using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyType enemyType = default;
    [SerializeField] private Renderer[] renderers;
    [SerializeField] private Material[] materials;
    public Unit Unit { get; private set; } = default;

    public int EnemyIndex { get; set; }
    
    private void Awake()
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

    public void SetControlPoint(ControlPointWithEnemies controlPoint)
    {
        RedirectDamageUnit(controlPoint.Unit);
        SetOwnLanes(controlPoint.ControlPointData.controlLanesFlags);
        SetBonuses(controlPoint.ControlPointData);
    }
    
    
    private void RedirectDamageUnit(Unit unitToRedirect) => Unit.RedirectDamageUnit = unitToRedirect;
    public void UnlockRedirectDamageUnit() => Unit.RedirectDamageUnit = null;

    private void SetOwnLanes(LanesFlags lanes) => Unit.OwnLaneFlags = lanes;
    private void SetAttackLanes(LanesFlags lanes) => Unit.AttackLaneFlags = lanes;

    private void SetBonuses(ControlPointData data)
    {
        Unit.stats.damage += (Unit.stats.damage * data.controlBonusDamage) / 100;
        Unit.stats.bonusRange += (Unit.stats.rangeAttack * data.controlBonusRange) / 100;
    }
    
    public void SetRenderers(int terrainIndex)
    {
        foreach (var rend in renderers)
        {
            rend.material = materials[terrainIndex];
        }
    }
    
}
