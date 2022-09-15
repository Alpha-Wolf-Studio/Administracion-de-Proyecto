using System;
using UnityEngine;

public class GamePlayManager : MonoBehaviourSingleton<GamePlayManager>
{
    public static Action<bool> OnGameOver;

    [SerializeField] private Unit unitToDestroy;
    [SerializeField] private TroopManager playerTroopManager;

    [SerializeField] private Unit[] currentLevelPrefabUnits = default;
    [SerializeField] private Projectile[] currentLevelPrefabProjectiles = default;

    public Unit[] CurrentLevelPrefabUnits => currentLevelPrefabUnits;
    public Projectile[] CurrentLevelPrefabProjectiles => currentLevelPrefabProjectiles;

    private bool isGameOver = false;

    private LevelData levelSelected;

    private new void Awake()
    {
        base.Awake();
        levelSelected = GameManager.Get().CurrentSelectedLevel;
    }

    private void Start()
    {
        Time.timeScale = 1; 
        unitToDestroy.OnDie += GameOverWin;
        AudioManager.Get().PlayMusicGamePlay();
        isGameOver = false;
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