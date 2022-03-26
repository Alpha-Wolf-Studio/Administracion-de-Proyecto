using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int maxEnemyLevel = 10;
    [SerializeField] private TroopManager enemyTroopManager;
    [SerializeField] private Vector2 spawnEnemyTime;
    private float nextTimeSpawn;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }
    private IEnumerator SpawnEnemy()
    {
        while (maxEnemyLevel > 0)
        {
            maxEnemyLevel--;
            enemyTroopManager.OnButtonCreateTroop(0);
            nextTimeSpawn = Random.Range(spawnEnemyTime.x, spawnEnemyTime.y);
            yield return new WaitForSeconds(nextTimeSpawn);
        }
    }
}