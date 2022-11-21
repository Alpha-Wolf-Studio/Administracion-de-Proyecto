using System;
using UnityEngine;

public class GamePlayManager : MonoBehaviourSingleton<GamePlayManager>
{
    public static Action<bool> OnGameOver;

    [SerializeField] private Unit unitToDestroy;
    [SerializeField] private float enemyBaseLife = 100;
    [SerializeField] private TroopManager playerTroopManager;

    [SerializeField] private Unit[] currentLevelPrefabUnits = default;
    [SerializeField] private Projectile[] currentLevelPrefabProjectiles = default;

    [Header("Forest Terrain")] 
    [SerializeField] private GameObject baseForestEnvironment;
    [SerializeField] private GameObject baseForestBurrowMesh;
    
    [Header("Desert Terrain")]
    [SerializeField] private GameObject baseDesertEnvironment;
    [SerializeField] private GameObject baseDesertBurrowMesh;
    
    [Header("Tundra Terrain")]
    [SerializeField] private GameObject baseTundraEnvironment;
    [SerializeField] private GameObject baseTundraBurrowMesh;
    
    
    public Unit[] CurrentLevelPrefabUnits => currentLevelPrefabUnits;
    public Projectile[] CurrentLevelPrefabProjectiles => currentLevelPrefabProjectiles;

    private bool isGameOver = false;

    private LevelData levelSelected;
    
    private void Start()
    {
        Time.timeScale = 1; 
        unitToDestroy.OnDie += GameOverWin;
        AudioManager.Get().PlayMusicGamePlay();
        isGameOver = false;

        UnitStats enemyBaseStats = new UnitStats();
        enemyBaseStats.life = enemyBaseLife;
        unitToDestroy.SetValues(enemyBaseStats, 0);
        
        levelSelected = GameManager.Get().CurrentSelectedLevel;
        
        if (IsTerrainAssigned() && IsBurrowAssigned())
            SetCurrentTerrain(levelSelected.TerrainType);
    }

    void GameOverWin()
    {
        if (!isGameOver)
        {
            GameManager.Get().OnLevelWin(levelSelected.Index);
            OnGameOver?.Invoke(true);
        }
    }

    void GameOverLose()
    {
        if (!isGameOver)
        {
            OnGameOver?.Invoke(false);
        }
    }

    public TroopManager GetPlayerTroopManager() => playerTroopManager;

    private void SetCurrentTerrain(TerrainGraphicType terrainGraphicType)
    {

            
        DeSelectAllTerrains();

        switch (terrainGraphicType)
        {
            case TerrainGraphicType.Desert:
                baseDesertEnvironment.SetActive(true);
                baseDesertBurrowMesh.SetActive(true);
                break;
            
            case TerrainGraphicType.Forest:
                baseForestEnvironment.SetActive(true);
                baseForestBurrowMesh.SetActive(true);
                break;
            
            case TerrainGraphicType.Snow:
                baseTundraEnvironment.SetActive(true);
                baseTundraBurrowMesh.SetActive(true);
                break;
            
            default:
                baseForestEnvironment.SetActive(true);
                baseForestBurrowMesh.SetActive(true);
                break;
        }
    }

    private void DeSelectAllTerrains()
    {
        baseForestEnvironment.SetActive(false);
        baseDesertEnvironment.SetActive(false);
        baseTundraEnvironment.SetActive(false);
        
        baseForestBurrowMesh.SetActive(false);
        baseDesertBurrowMesh.SetActive(false);
        baseTundraBurrowMesh.SetActive(false);
    }

    private bool IsTerrainAssigned()
    {
        return baseForestEnvironment.activeSelf && baseDesertEnvironment.activeSelf && baseTundraEnvironment.activeSelf;
    }

    private bool IsBurrowAssigned()
    {
        return baseForestBurrowMesh.activeSelf && baseDesertBurrowMesh.activeSelf && baseTundraBurrowMesh.activeSelf;
    }
    
}