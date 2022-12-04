using System;
using UnityEngine;
using UnityEngine.UI;

public class UiManagerMilitaryBase : MonoBehaviour
{
    [SerializeField] private Button btnGoToCampaing;
    private AudioSource audioSource;

    private void Awake ()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start ()
    {
        btnGoToCampaing.onClick.AddListener(GoToMenuCampaing);
    }

    public void GoToMenuCampaing ()
    {
        CustomSceneManager.Get().LoadScene("Campaign");
        audioSource.Play();
    } 

    //------------------------------------------------------------------------------------------------
    [Space(15)]
    [SerializeField] private UpgradeBase btnBuyUnit;
    [SerializeField] private UpgradeBase btnBuySlot;
    [SerializeField] private UpgradeBase btnUpgradeUnit;
    [SerializeField] private UpgradeBase btnHealthUnits;
    [SerializeField] private Button btnFilterMercenary;
    [SerializeField] private Button btnFilterGranade;

    public void DoExceptBuyUnit () => btnBuyUnit.BuyUpgrade();
    public void DoExceptBuySlot() => btnBuySlot.BuyUpgrade();
    public void DoExceptBuyUpgradeUnit() => btnUpgradeUnit.BuyUpgrade();
    public void DoExceptBuyHealthUnit() => btnHealthUnits.BuyUpgrade();
    public void DoExceptFilterMercenary() => btnFilterMercenary.onClick.Invoke();
    public void DoExceptFilterGranade() => btnFilterGranade.onClick.Invoke();
}