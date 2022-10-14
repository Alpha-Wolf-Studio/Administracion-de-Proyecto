using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private LayerMask alliesLayer = default;
    [SerializeField] private int enemiesLayerIndex = 8;
    [SerializeField] private List<Unit> enemyPrefabs;
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

        }
    }

    public void SaveAllEnemiesInLevel()
    {
        currentLevel.Enemies.Clear();
        
        var newEnemies = FindObjectsOfType<Enemy>();
        foreach (var enemy in newEnemies)
        {
            currentLevel.Enemies.Add(enemy.GetCurrentConfiguration());
        }
        
        GameManager.Get().SaveEnemyLevelData(currentLevel.Enemies, currentLevel.Index);
    }
    
    public void ClearAllEnemiesInLevel()
    {
        currentLevel.Enemies.Clear();
        
        var newEnemies = FindObjectsOfType<Enemy>();
        foreach (var enemy in newEnemies)
        {
            Destroy(enemy.gameObject);
        }
    }
    

}