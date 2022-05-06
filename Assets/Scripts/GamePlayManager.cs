using System;
using UnityEngine;

public class GamePlayManager : MonoBehaviourSingleton<GamePlayManager>
{
    public static Action<bool> OnGameOver;

    [SerializeField] private Unit unitToDestroy;
    [SerializeField] private Unit unitToDefend;
    [SerializeField] private TroopManager playerTroopManager;
    private void Start()
    {
        Time.timeScale = 1; 
        unitToDestroy.OnDie += GameOverWin;
        unitToDefend.OnDie += GameOverLose;
        AudioManager.Get().PlayMusicGamePlay();
    }

    void GameOverWin()
    {
        if (!GameManager.Get().gameover)
        {
            GameManager.Get().gameover = true;
            OnGameOver?.Invoke(true);
        }
    }

    void GameOverLose()
    {
        if (!GameManager.Get().gameover)
        {
            GameManager.Get().gameover = true;
            OnGameOver?.Invoke(false);
        }
    }

    public TroopManager GetPlayerTroopManager() => playerTroopManager;
}