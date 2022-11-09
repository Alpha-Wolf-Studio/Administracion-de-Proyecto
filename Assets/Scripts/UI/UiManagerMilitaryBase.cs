using UnityEngine;
using UnityEngine.UI;

public class UiManagerMilitaryBase : MonoBehaviour
{
    [SerializeField] private Button btnGoToCampaing;

    void Start ()
    {
        btnGoToCampaing.onClick.AddListener(GoToMenuCampaing);
    }

    public void GoToMenuCampaing ()=> CustomSceneManager.Get().LoadScene("Campaign");

    //------------------------------------------------------------------------------------------------
    [Space(15)]
    [SerializeField] private Button btnBuyUnit;
    [SerializeField] private Button btnBuySlot;
    [SerializeField] private Button btnUpgradeUnit;
    [SerializeField] private Button btnHealthUnits;
    [SerializeField] private Button btnFilterMercenary;
    [SerializeField] private Button btnFilterGranade;

    public void DoExceptBuyUnit() => btnBuyUnit.onClick.Invoke();
    public void DoExceptBuySlot() => btnBuySlot.onClick.Invoke();
    public void DoExceptBuyUpgradeUnit() => btnUpgradeUnit.onClick.Invoke();
    public void DoExceptBuyHealthUnit() => btnHealthUnits.onClick.Invoke();
    public void DoExceptFilterMercenary() => btnFilterMercenary.onClick.Invoke();
    public void DoExceptFilterGranade() => btnFilterGranade.onClick.Invoke();
}