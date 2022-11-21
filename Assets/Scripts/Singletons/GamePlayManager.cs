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
    
    [Header("Terrains")] 
    [SerializeField] private GameObject baseForestTerrain;
    [SerializeField] private GameObject baseDesertTerrain;
    [SerializeField] private GameObject baseTundraTerrain;
    
    
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
                baseDesertTerrain.SetActive(true);
                break;
            
            case TerrainGraphicType.Forest:
                baseForestTerrain.SetActive(true);
                break;
            
            case TerrainGraphicType.Snow:
                baseTundraTerrain.SetActive(true);
                break;
            
            default:
                baseForestTerrain.SetActive(true);
                break;
        }
        
    }

    private void DeSelectAllTerrains()
    {
        baseForestTerrain.SetActive(false);
        baseDesertTerrain.SetActive(false);
        baseTundraTerrain.SetActive(false);
    }
    
}