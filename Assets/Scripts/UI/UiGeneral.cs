using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiGeneral : MonoBehaviourSingleton<UiGeneral>
{
    [SerializeField] private TMP_Text textVersion;
    [SerializeField] private TMP_Text textIncome;
    [SerializeField] private TMP_Text textCurrentGold;
    [SerializeField] private TMP_Text textCurrentDiamond;
    [SerializeField] private TMP_Text textNickName;
    [SerializeField] private GameObject panelProfile;
    [SerializeField] private GameObject panelSettings;
    [SerializeField] private Button btnProfile;
    [SerializeField] private Button btnSettings;

    [SerializeField] private List<GameObject> hideToGameplay = new List<GameObject>();

    private void Start ()
    {
        GoldCalculations.OnIncomeGoldChange += ChangeCurrentIncome;
        textNickName.text = GameManager.Get().GetPlayerName();
        btnProfile.onClick.AddListener(OnPressProfile);
        btnSettings.onClick.AddListener(OnPressSettings);

        textVersion.text = "Version: " + Application.version;
        
    }

    private void Update ()
    {
        textCurrentGold.text = GameManager.Get().GetPlayerGold().ToString("F0");
        textCurrentDiamond.text = GameManager.Get().GetPlayerDiamond().ToString("F0");
    }

    private void ChangeCurrentIncome ()
    {
        textIncome.text = GoldCalculations.IncomeGold.ToString("F0");
    }

    private void OnPressProfile ()
    {
        panelProfile.SetActive(true);
    }

    private void OnPressSettings ()
    {
        Time.timeScale = 0;
        panelSettings.SetActive(true);
    }

    public void HideToGameplay (bool hide)
    {
        for (int i = 0; i < hideToGameplay.Count; i++)
        {
            hideToGameplay[i].SetActive(hide);
        }
    }
}