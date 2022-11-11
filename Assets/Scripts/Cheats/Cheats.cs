using System;
using UnityEngine;
using UnityEngine.UI;

public class Cheats : MonoBehaviour
{
    [SerializeField] private Button btnActiveTutorial;
    [SerializeField] private Button btnCheatOpener;
    [SerializeField] private Button btnGold;
    [SerializeField] private Button btnDiam;
    [SerializeField] private Button btnTerritory;

    [SerializeField] private GameObject panelCheats;

    private PlayerCampaignManager playerCampaignManager;

    private bool isOpen;

    private int valueAddGold = 500;
    private int valueAddDiam = 50;


    private void Start ()
    {
        btnActiveTutorial.onClick.AddListener(ActivateTutorial);
        panelCheats.SetActive(isOpen);

        btnCheatOpener.onClick.AddListener(PanelCheats);
        btnGold.onClick.AddListener(AddGold);
        btnDiam.onClick.AddListener(AddDiam);

        playerCampaignManager = FindObjectOfType<PlayerCampaignManager>();
        if (playerCampaignManager)
            btnTerritory.onClick.AddListener(AddTerritory);
        else
            btnTerritory.gameObject.SetActive(false);
    }

    private void OnDestroy ()
    {
        btnCheatOpener.onClick.RemoveAllListeners();
        btnGold.onClick.RemoveAllListeners();
        btnDiam.onClick.RemoveAllListeners();
        btnTerritory.onClick.RemoveAllListeners();
    }

    private void PanelCheats ()
    {
        isOpen = !isOpen;
        panelCheats.SetActive(isOpen);
    }

    private void AddGold ()
    {
        GameManager.Get().ModifyGoldPlayer(valueAddGold);
    }

    private void AddDiam ()
    {
        GameManager.Get().ModifyDiamondPlayer(valueAddDiam);
    }

    private void AddTerritory ()
    {
        playerCampaignManager.CompleteCurrentLevel();
    }

    void ActivateTutorial ()
    {
        TutorialManager.Get().TestInitialTutorial = true;
    }
}