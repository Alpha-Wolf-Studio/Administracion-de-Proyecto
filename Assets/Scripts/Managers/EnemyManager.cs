using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public static System.Action OnLevelLoaded;

    [SerializeField] private LayerMask alliesLayer = default;
    [SerializeField] private int enemiesLayerIndex = 8;
    [SerializeField] private List<Unit> enemyPrefabs;
    [SerializeField] private List<ControlPointWithEnemies> controlPointPrefabs;
    private LevelData currentLevel = default;

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

            var unitShootBehaviour = unit.GetComponent<UnitShootBehaviour>();
            if (unitShootBehaviour) 
            {
                Projectile currentProjectilePrefab = prefabProjectiles[(int)unitStats.attackType];
                unitShootBehaviour.SetPrefabProjectile(currentProjectilePrefab);
            }

            unit.AttackLaneFlags = enemy.AttackLaneFlags;
            unit.OwnLaneFlags = enemy.OwnLaneFlags;

            var enemyComponent = unit.GetComponent<Enemy>();
            enemyComponent.EnemyIndex = enemy.EnemyIndex;
        }

        foreach (var controlPoint in currentLevel.ControlPoints)
        {
            int lanesAmount = controlPoint.GetControlLanesAmount();
            var controlPointGo = Instantiate(controlPointPrefabs[lanesAmount], controlPoint.ControlPosition, Quaternion.Euler(controlPoint.ControlRotation));
            if (controlPoint.ControlEnemiesIndex.Count <= 0) continue;
            var controlPointEnemies = new List<Enemy>();
            controlPointGo.AssignData(controlPoint.ControlData, controlPointEnemies);
        }
        
        OnLevelLoaded?.Invoke();
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

        var controlPoints = FindObjectsOfType<ControlPointWithEnemies>();
        foreach (var controlPoint in controlPoints)
        {
            currentLevel.ControlPoints.Add(controlPoint.GetCurrentConfiguration());
        }
        
        
        GameManager.Get().SaveEnemyLevelData(currentLevel.Enemies, currentLevel.Index);
        GameManager.Get().SaveControlPointsLevelData(currentLevel.ControlPoints, currentLevel.Index);
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