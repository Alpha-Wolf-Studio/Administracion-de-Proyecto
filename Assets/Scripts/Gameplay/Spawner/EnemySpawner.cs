using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private EnemySpawnerConfiguration configuration;
    [SerializeField] private EnemySpawnerTrigger triggerForSpawning;

    public EnemySpawnerConfiguration Configuration
    {
        get => configuration;
        set
        {
            configuration = value;
            RecalculateSpawnType();
        } 
    }
    
    private readonly Vector3 topPosition = new Vector3(100, 0, 10);
    private readonly Vector3 midPosition = new Vector3(100, 0, 0);
    private readonly Vector3 botPosition = new Vector3(100, 0, -10);
    
    private EnemyManager enemyManager;
    private bool triggered = false;

    private void Awake()
    {
        enemyManager = FindObjectOfType<EnemyManager>();
        triggerForSpawning.gameObject.SetActive(false);
    }

    private void Start()
    {
        RecalculateSpawnType();
        
        triggerForSpawning.OnTriggered += delegate
        {
            triggered = true;
        };
    }

    private void OnValidate()
    {
        if(triggerForSpawning)
            triggerForSpawning.gameObject.SetActive(Configuration.spawnerClass == SpawnerClass.Trigger);
        
        if(Application.isPlaying)
            RecalculateSpawnType();
    }

    private void RecalculateSpawnType()
    { 
        
        StopAllCoroutines();
        if (Configuration.enemyToSpawn == EnemyType.Size || Configuration.spawnerClass == SpawnerClass.None) return;
        
        switch (Configuration.spawnerClass)
        {
            case SpawnerClass.Timer:
                StartCoroutine(SpawnWithTimeCoroutine());
                break;
            case SpawnerClass.Trigger:
                StartCoroutine(SpawnWithColliderCoroutine());
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    private IEnumerator SpawnWithColliderCoroutine()
    {
        triggerForSpawning.gameObject.SetActive(true);

        while (!triggered)
        {
            yield return null;
        }
        
        triggerForSpawning.gameObject.SetActive(false);
        
        do
        {
            SpawnLogic();
            yield return new WaitForSeconds(Configuration.timeForTriggerSpawn);
        } while (Configuration.repeatAfterTrigger);
    }

    private IEnumerator SpawnWithTimeCoroutine()
    {
        do
        {
            yield return new WaitForSeconds(Configuration.timeForSpawn);
            SpawnLogic();
        } while (Configuration.repeatAfterTime);
    }

    private void SpawnLogic()
    {
        if (Configuration.spawnLanes.HasFlag(LanesFlags.Bottom))
            StartCoroutine(SpawnLogicCoroutine(Configuration.enemyToSpawn, botPosition, Quaternion.identity, LanesFlags.Bottom));

        if(Configuration.spawnLanes.HasFlag(LanesFlags.Mid))
            StartCoroutine(SpawnLogicCoroutine(Configuration.enemyToSpawn, midPosition, Quaternion.identity, LanesFlags.Mid));
        
        if(Configuration.spawnLanes.HasFlag(LanesFlags.Top))
            StartCoroutine(SpawnLogicCoroutine(Configuration.enemyToSpawn, topPosition, Quaternion.identity, LanesFlags.Top));
    }

    private IEnumerator SpawnLogicCoroutine(EnemyType enemyType, Vector3 position, Quaternion rotation, LanesFlags lane)
    {
        float variance = Random.Range(0, Configuration.spawnVariance);
        yield return new WaitForSeconds(variance);
        enemyManager.SpawnEnemy(enemyType, position, rotation, lane, transform);
    }
    
    
    public enum SpawnerClass
    {
        Timer,
        Trigger,
        None
    }


}
