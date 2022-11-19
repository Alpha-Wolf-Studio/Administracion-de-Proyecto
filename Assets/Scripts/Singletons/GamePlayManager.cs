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
}