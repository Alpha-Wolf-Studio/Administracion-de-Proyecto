using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public static System.Action OnLevelLoaded;

    [SerializeField] private LayerMask alliesLayer = default;
    [SerializeField] private int enemiesLayerIndex = 8;
    [SerializeField] private List<Unit> enemyPrefabs;
    [SerializeField] private ControlPointWithEnemies controlPointPrefab;
    private LevelData currentLevel = default;

    private List<Enemy> enemies = new List<Enemy>();
    private List<ControlPointWithEnemies> controlPoints = new List<ControlPointWithEnemies>();

    void Start()
    {
        currentLevel = GameManager.Get().CurrentSelectedLevel;
        foreach (var enemy in currentLevel.Enemies)
        {
            var unit = Instantiate(enemyPrefabs[(int)enemy.TypeOfEnemy], enemy.EnemyPosition, Quaternion.Euler(enemy.EnemyRotation));
                
            var prefabProjectiles = GamePlayManager.Get().CurrentLevelPrefabProjectiles;

            unit.gameObject.layer = enemiesLayerIndex;
            unit.signDirection = -1;
            unit.interactableMask = 0;
            unit.enemyMask = alliesLayer;

            UnitStats unitStats = GameManager.Get().GetUnitStats((int)enemy.TypeOfEnemy);
            unit.SetValues(unitStats, 0);

            var unitShootBehaviour = unit.GetComponent<IShootBehaviour>();
            if (unitShootBehaviour != null) 
            {
                Projectile currentProjectilePrefab = prefabProjectiles[(int)unitStats.attackType];
                unitShootBehaviour.SetPrefabProjectile(currentProjectilePrefab);
            }

            unit.AttackLaneFlags = enemy.AttackLaneFlags;
            unit.OwnLaneFlags = enemy.OwnLaneFlags;

            var enemyComponent = unit.GetComponent<Enemy>();
            enemyComponent.EnemyIndex = enemy.EnemyIndex;
            enemies.Add(enemyComponent);
        }

        foreach (var controlPoint in currentLevel.ControlPoints)
        {
            var controlPointGo = Instantiate(controlPointPrefab, controlPoint.ControlPosition, Quaternion.Euler(controlPoint.ControlRotation));
            controlPoints.Add(controlPointGo);
            
            if (controlPoint.ControlEnemiesIndex.Count <= 0) continue;

            var controlPointEnemies = new List<Enemy>();
            
            foreach (var enemyIndex in controlPoint.ControlEnemiesIndex)
            {
                var enemy = enemies.Find(i => i.EnemyIndex == enemyIndex);
                controlPointEnemies.Add(enemy);
            }
            
            controlPointGo.AssignData(controlPoint.ControlData, controlPointEnemies);
        }
        
        OnLevelLoaded?.Invoke();
    }
    
    public void SpawnEnemy(EnemyType enemyType, Vector3 position, Quaternion rotation, LanesFlags lanes, Transform trans)
    {
        var prefabProjectiles = GamePlayManager.Get().CurrentLevelPrefabProjectiles;
        
        var unit = Instantiate(enemyPrefabs[(int)enemyType], position, rotation, trans);

        unit.gameObject.layer = enemiesLayerIndex;
        unit.signDirection = -1;
        unit.interactableMask = 0;
        unit.enemyMask = alliesLayer;

        UnitStats unitStats = GameManager.Get().GetUnitStats((int)enemyType);
        unit.SetValues(unitStats, 0);

        var unitShootBehaviour = unit.GetComponent<IShootBehaviour>();
        if (unitShootBehaviour != null) 
        {
            Projectile currentProjectilePrefab = prefabProjectiles[(int)unitStats.attackType];
            unitShootBehaviour.SetPrefabProjectile(currentProjectilePrefab);
        }

        unit.AttackLaneFlags = lanes;
        unit.OwnLaneFlags = lanes;
    }
    
    public void SaveAllDataInLevel()
    {
        currentLevel.Enemies.Clear();
        
        var newEnemies = FindObjectsOfType<Enemy>();

        for (int i = 0; i < newEnemies.Length; i++)
        {
            newEnemies[i].EnemyIndex = i;
            currentLevel.Enemies.Add(newEnemies[i].GetCurrentConfiguration());
        }

        currentLevel.ControlPoints.Clear();
        var controlPoints = FindObjectsOfType<ControlPointWithEnemies>();
        foreach (var controlPoint in controlPoints)
        {
            currentLevel.ControlPoints.Add(controlPoint.GetCurrentConfiguration());
        }

        GameManager.Get().SaveLevelEnemiesData();
    }
    
    public void ClearAllDataInLevel()
    {
        currentLevel.Enemies.Clear();
        currentLevel.ControlPoints.Clear();
        
        var newEnemies = FindObjectsOfType<Enemy>();
        foreach (var enemy in newEnemies)
        {
            Destroy(enemy.gameObject);
        }
        
        var controlPoints = FindObjectsOfType<ControlPointWithEnemies>();
        foreach (var controlPoint in controlPoints)
        {
            Destroy(controlPoint.gameObject);
        }
    }
    

}