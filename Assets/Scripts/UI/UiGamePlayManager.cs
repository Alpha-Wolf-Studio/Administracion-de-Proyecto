using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiGamePlayManager : MonoBehaviour
{

    [Header("Enemies")] 
    [SerializeField] private EnemyManager enemyManager = default;
    [SerializeField] private GameObject enemiesDebugPanel = default; 
    [SerializeField] private Button saveAllCurrentEnemiesButton = default;
    [SerializeField] private Button clearAllCurrentEnemiesButton = default;
    [Space(10)]
    
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

    private UiGeneral uiGeneral;
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
        
#if UNITY_EDITOR
        saveAllCurrentEnemiesButton.onClick.AddListener(enemyManager.SaveAllEnemiesInLevel);
        clearAllCurrentEnemiesButton.onClick.AddListener(enemyManager.ClearAllEnemiesInLevel);
#else
        enemiesDebugPanel.gameObject.SetActive(false);
#endif
        uiGeneral = FindObjectOfType<UiGeneral>();
        if (uiGeneral)
        {
            uiGeneral.HideToGameplay(true);
        }
    }

    private void OnDestroy()
    {
#if UNITY_EDITOR
        saveAllCurrentEnemiesButton.onClick.RemoveListener(enemyManager.SaveAllEnemiesInLevel);
        clearAllCurrentEnemiesButton.onClick.RemoveListener(enemyManager.ClearAllEnemiesInLevel);
#endif
        GamePlayManager.OnGameOver -= GameOverUi;

        if (uiGeneral)
        {
            uiGeneral.HideToGameplay(false);
        }
    }

    private void OnButtonToMainMenu() => CustomSceneManager.Get().LoadScene("Campaign");
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