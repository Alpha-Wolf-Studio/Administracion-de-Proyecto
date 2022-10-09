using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Spawn Configurations")]
    [SerializeField] private int maxEnemyLevel = 10;
    [SerializeField] private TroopManager enemyTroopManager = default;
    [SerializeField] private Vector2 spawnEnemyTime = Vector2.zero;
    [SerializeField] private bool autoSpawnEnemy = true;
    private float nextTimeSpawn = 0;

    [Header("Trenches Configurations")]
    [SerializeField] private float maxTimeToUnloadTrenches = 10f;
    private Trench[] levelTrenches = default;
    private float timeToUnloadTrenches = 0f;

    void Start()
    {
        levelTrenches = FindObjectsOfType<Trench>();
        timeToUnloadTrenches = maxTimeToUnloadTrenches;

        if(autoSpawnEnemy) StartCoroutine(SpawnRandomEnemy());
    }

    private void Update()
    {
        TrenchReleaseLogic();
    }

    private void TrenchReleaseLogic() 
    {
        timeToUnloadTrenches -= Time.deltaTime;
        if(timeToUnloadTrenches < 0) 
        {
            timeToUnloadTrenches = maxTimeToUnloadTrenches;
            foreach (var trench in levelTrenches)
            {
                if(trench.CurrentTroopsLayer == gameObject.layer) 
                {
                    trench.ReleaseUnitsFromTrench();
                }
            }
        }
    }

    private IEnumerator SpawnRandomEnemy()
    {
        while (maxEnemyLevel > 0)
        {
            nextTimeSpawn = Random.Range(spawnEnemyTime.x, spawnEnemyTime.y);
            yield return new WaitForSeconds(nextTimeSpawn);
            maxEnemyLevel--;
            int currentSpawn = Random.Range(0, GamePlayManager.Get().CurrentLevelPrefabUnits.Length);
            enemyTroopManager.OnButtonCreateTroop(currentSpawn);
        }
    }
}