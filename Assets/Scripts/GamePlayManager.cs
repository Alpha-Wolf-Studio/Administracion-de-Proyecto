using System;
using UnityEngine;

public class GamePlayManager : MonoBehaviourSingleton<GamePlayManager>
{
    public static Action<bool> OnGameOver;

    [SerializeField] private Unit unitToDestroy;
    [SerializeField] private Unit unitToDefend;

    private void Start()
    {
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
}