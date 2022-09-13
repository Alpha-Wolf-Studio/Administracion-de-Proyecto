using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiGamePlayManager : MonoBehaviour
{
    [SerializeField] private Button[] btnToMenu;
    [SerializeField] private Button[] btnToReset;
    [SerializeField] private Button btnToCampaign;

    [SerializeField] private Button btnPause;
    [SerializeField] private Button btnUnPause;

    [SerializeField] private CanvasGroup uiPanelGameplay;
    [SerializeField] private CanvasGroup uiPanelPause;
    [SerializeField] private CanvasGroup uiPanelWin;
    [SerializeField] private CanvasGroup uiPanelLose;

    [Space(10)]
    [SerializeField] private TMPro.TextMeshProUGUI levelTextComponent;


    private void Start()
    {
        uiPanelWin.gameObject.SetActive(false);
        uiPanelLose.gameObject.SetActive(false);

        foreach (Button btnMenu in btnToMenu)
            btnMenu.onClick.AddListener(OnButtonToMainMenu);
        foreach (Button btnReset in btnToReset)
            btnReset.onClick.AddListener(OnButtonReset);

        btnToCampaign.onClick.AddListener(OnButtonCampaign);

        btnPause.onClick.AddListener(OnButtonPause);
        btnUnPause.onClick.AddListener(OnButtonDisablePause);

        GamePlayManager.OnGameOver += GameOverUi;

        levelTextComponent.text = GameManager.Get().CurrentSelectedLevel.LevelName;

    }

    private void OnDestroy()
    {
        GamePlayManager.OnGameOver -= GameOverUi;
    }

    private void OnButtonToMainMenu() => CustomSceneManager.Get().LoadScene("MainMenu");
    private void OnButtonReset() => CustomSceneManager.Get().LoadScene("Gameplay");
    private void OnButtonCampaign() => CustomSceneManager.Get().LoadScene("Campaign");

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