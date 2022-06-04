using System;
using UnityEngine;

public class GamePlayManager : MonoBehaviourSingleton<GamePlayManager>
{
    public static Action<bool> OnGameOver;

    [SerializeField] private int currentLevel = 1;
    [SerializeField] private Unit unitToDestroy;
    [SerializeField] private Unit unitToDefend;
    [SerializeField] private TroopManager playerTroopManager;

    [SerializeField] private Unit[] currentLevelPrefabUnits = default;
    [SerializeField] private Projectile[] currentLevelPrefabProjectiles = default;

    public Unit[] CurrentLevelPrefabUnits => currentLevelPrefabUnits;
    public Projectile[] CurrentLevelPrefabProjectiles => currentLevelPrefabProjectiles;

    private bool isGameOver = false;

    public int CurrentLevel => currentLevel;

    private void Start()
    {
        Time.timeScale = 1; 
        unitToDestroy.OnDie += GameOverWin;
        unitToDefend.OnDie += GameOverLose;
        AudioManager.Get().PlayMusicGamePlay();
        isGameOver = false;
    }

    void GameOverWin()
    {
        if (!isGameOver)
        {
            GameManager.Get().CompleteLevelPlayer(currentLevel);
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