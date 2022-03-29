using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Unit unitToDestroy;
    [SerializeField] private Unit unitToDefend;
    private bool gameover;
    [SerializeField] private TextMeshProUGUI textWin;
    private void Start()
    {
        unitToDestroy.OnDie += GameOverWin;
        unitToDefend.OnDie += GameOverLose;
    }
    void GameOverWin()
    {
        if (!gameover)
        {
            gameover = true;
            textWin.text = "You Win!";
        }
    }
    void GameOverLose()
    {
        if (!gameover)
        {
            gameover = true;
            textWin.text = "You Lose!";
        }
    }
}