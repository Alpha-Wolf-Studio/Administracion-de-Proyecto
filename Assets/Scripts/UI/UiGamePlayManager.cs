using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiGamePlayManager : MonoBehaviour
{

    [Header("Level Data")] 
    [SerializeField] private EnemyManager enemyManager = default;
    [SerializeField] private GameObject enemiesDebugPanel = default;
    [SerializeField] private Button saveAllCurrentEnemiesButton = default;
    [SerializeField] private Button resetAllCurrentEnemiesButton = default;
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

    [Header("Win Panel")] 
    [SerializeField] private GameObject panelWinGold;
    [SerializeField] private GameObject panelWinIncomeGold;
    [SerializeField] private GameObject panelWinDiamond;
    
    [Space(10)]
    [SerializeField] private TextMeshProUGUI levelTextComponent;

    private UiGeneral uiGeneral;
    private TextMeshProUGUI winGoldText;
    private TextMeshProUGUI winGoldIncomeText;
    private TextMeshProUGUI WinDiamondText;
    
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
        saveAllCurrentEnemiesButton.onClick.AddListener(enemyManager.SaveAllDataInLevel);
        clearAllCurrentEnemiesButton.onClick.AddListener(enemyManager.ClearAllDataInLevel);
        resetAllCurrentEnemiesButton.onClick.AddListener(enemyManager.ResetAllDataInLevel);
#else
        enemiesDebugPanel.gameObject.SetActive(false);
#endif
        uiGeneral = FindObjectOfType<UiGeneral>();
        if (uiGeneral)
        {
            uiGeneral.HideToGameplay(true);
        }

        winGoldText = panelWinGold.GetComponentInChildren<TextMeshProUGUI>();
        winGoldIncomeText = panelWinIncomeGold.GetComponentInChildren<TextMeshProUGUI>();
        WinDiamondText = panelWinDiamond.GetComponentInChildren<TextMeshProUGUI>();
        
    }

    private void OnDestroy()
    {
#if UNITY_EDITOR
        saveAllCurrentEnemiesButton.onClick.RemoveListener(enemyManager.SaveAllDataInLevel);
        clearAllCurrentEnemiesButton.onClick.RemoveListener(enemyManager.ClearAllDataInLevel);
        resetAllCurrentEnemiesButton.onClick.RemoveListener(enemyManager.ResetAllDataInLevel);
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
        if (isWin)
        {
            var currentSelectedLevel = GameManager.Get().CurrentSelectedLevel;

            int currentWonGold = currentSelectedLevel.GoldOnComplete;
            int currentWonIncomeGold = currentSelectedLevel.GoldIncome;
            int currentWonDiamond = currentSelectedLevel.DiamondOnComplete;
            
            panelWinGold.SetActive(currentWonGold > 0);
            panelWinIncomeGold.SetActive(currentWonIncomeGold > 0);
            panelWinDiamond.SetActive(currentWonDiamond > 0);

            winGoldText.text = currentWonGold.ToString();
            winGoldIncomeText.text = currentWonIncomeGold.ToString();
            WinDiamondText.text = currentWonDiamond.ToString();
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