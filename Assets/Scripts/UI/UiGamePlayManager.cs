using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiGamePlayManager : MonoBehaviour
{
    [SerializeField] private Button[] btnToMenu;
    [SerializeField] private Button[] btnToReset;
    [SerializeField] private Button btnToContinue;

    [SerializeField] private Button btnPause;
    [SerializeField] private Button btnUnPause;

    [SerializeField] private CanvasGroup uiPanelGameplay;
    [SerializeField] private CanvasGroup uiPanelPause;
    [SerializeField] private CanvasGroup uiPanelWin;
    [SerializeField] private GameObject uiPanelWinNormal;
    [SerializeField] private GameObject uiPanelWinLastLevel;
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

        btnPause.onClick.AddListener(OnButtonPause);
        btnUnPause.onClick.AddListener(OnButtonDisablePause);
        btnToContinue.onClick.AddListener(OnButtonContinue);

        GamePlayManager.OnGameOver += GameOverUi;

        levelTextComponent.text = "Mission " + GamePlayManager.Get().CurrentMission.ToString();

    }

    private void OnDestroy()
    {
        GamePlayManager.OnGameOver -= GameOverUi;
    }

    private void OnButtonToMainMenu() => SceneManager.LoadScene("MainMenu");
    private void OnButtonReset() => SceneManager.LoadScene("Level " + GamePlayManager.Get().CurrentMission.ToString());
    private void OnButtonContinue() => SceneManager.LoadScene("Level " + (GamePlayManager.Get().CurrentMission + 1).ToString());

    void GameOverUi(bool isWin)
    {
        uiPanelWin.gameObject.SetActive(isWin);
        uiPanelLose.gameObject.SetActive(!isWin);

        if (isWin) 
        {
            if(GamePlayManager.Get().CurrentMission == GameManager.Get().GetCurrentMissionsAmount()) 
            {
                uiPanelWinLastLevel.SetActive(true);
                uiPanelWinNormal.SetActive(false);
            }
            else 
            {
                uiPanelWinLastLevel.SetActive(false);
                uiPanelWinNormal.SetActive(true);
            }
        }

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