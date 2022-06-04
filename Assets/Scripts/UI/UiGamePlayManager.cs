using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiGamePlayManager : MonoBehaviour
{
    [SerializeField] private Button[] btnToMenu;
    [SerializeField] private Button[] btnToReset;

    [SerializeField] private Button btnPause;
    [SerializeField] private Button btnUnPause;

    [SerializeField] private CanvasGroup uiPanelGameplay;
    [SerializeField] private CanvasGroup uiPanelPause;
    [SerializeField] private CanvasGroup uiPanelWin;
    [SerializeField] private CanvasGroup uiPanelLose;

    private void Start()
    {
        uiPanelWin.gameObject.SetActive(false);
        uiPanelLose.gameObject.SetActive(false);

        foreach (Button btnMenu in btnToMenu)
            btnMenu.onClick.AddListener(OnButtonToMainMenu);
        foreach (Button btnReset in btnToReset)
            btnReset.onClick.AddListener(OnButtonContinue);

        btnPause.onClick.AddListener(OnButtonPause);
        btnUnPause.onClick.AddListener(OnButtonDisablePause);

        GamePlayManager.OnGameOver += GameOverUi;
    }

    private void OnButtonToMainMenu() => SceneManager.LoadScene("MainMenu");
    private void OnButtonContinue() => SceneManager.LoadScene("Level " + (GamePlayManager.Get().CurrentLevel + 1).ToString());

    void GameOverUi(bool isWin)
    {
        uiPanelWin.gameObject.SetActive(isWin);
        uiPanelLose.gameObject.SetActive(!isWin);
    }

    void OnButtonPause()
    {
        Time.timeScale = 0;
        uiPanelGameplay.gameObject.SetActive(false);
        uiPanelPause.gameObject.SetActive(true);
    }

    void OnButtonDisablePause()
    {
        Time.timeScale = 1;
        uiPanelGameplay.gameObject.SetActive(true);
        uiPanelPause.gameObject.SetActive(false);
    }
}