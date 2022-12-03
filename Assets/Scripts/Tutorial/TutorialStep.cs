using UnityEngine;
using UnityEngine.UI;

public class TutorialStep : MonoBehaviour
{
    public event System.Action OnStepDone;

    public OtterAnimation otterAnimation;

    [SerializeField] private Button btnHit;
    [SerializeField] private bool hasArrow;
    [SerializeField] private Transform arrow;
    [SerializeField] private StepType doBehaviour = StepType.Continue;
    [SerializeField] private Button btnSkip;

    private void Awake ()
    {
        otterAnimation.OnEndWriting += OnEndWriting;
    }

    private void Start ()
    {
        btnHit.onClick.AddListener(SetToDoneStep);
        
        if(btnSkip)
            btnSkip.onClick.AddListener(SkipWriting);
    }

    void SkipWriting ()
    {
        if(btnSkip)
            btnSkip.onClick.RemoveListener(SkipWriting);
        otterAnimation.EndWriting();
    }

    void SetToDoneStep ()
    {
        btnHit.onClick.RemoveListener(SetToDoneStep);

        UiManagerCampaign uiManagerCampaign = FindObjectOfType<UiManagerCampaign>();
        UiManagerMilitaryBase uiManagerMilitaryBase = FindObjectOfType<UiManagerMilitaryBase>();

        switch (doBehaviour)
        {
            case StepType.Continue:

                break;
            case StepType.GoCampaignToMilitaryBase:
                if (uiManagerCampaign) uiManagerCampaign.GoToMenuMilitaryBase();
                else Debug.Log("No existe el objeto referenciado" + gameObject);
                break;
            case StepType.BuyUnit:
                if (uiManagerMilitaryBase) uiManagerMilitaryBase.DoExceptBuyUnit();
                else Debug.Log("No existe el objeto referenciado" + gameObject);
                break;
            case StepType.BuySlot:
                if (uiManagerMilitaryBase) uiManagerMilitaryBase.DoExceptBuySlot();
                else Debug.Log("No existe el objeto referenciado" + gameObject);
                break;
            case StepType.BuyUpgrade:
                if (uiManagerMilitaryBase) uiManagerMilitaryBase.DoExceptBuyUpgradeUnit();
                else Debug.Log("No existe el objeto referenciado" + gameObject);
                break;
            case StepType.BuyHealth:
                if (uiManagerMilitaryBase) uiManagerMilitaryBase.DoExceptBuyHealthUnit();
                else Debug.Log("No existe el objeto referenciado" + gameObject);
                break;
            case StepType.GoMilitaryBaseToCampaing:
                if (uiManagerMilitaryBase) uiManagerMilitaryBase.GoToMenuCampaing();
                else Debug.Log("No existe el objeto referenciado" + gameObject);
                break;
            case StepType.ChangeToGranade:
                if (uiManagerMilitaryBase) uiManagerMilitaryBase.DoExceptFilterMercenary();
                else Debug.Log("No existe el objeto referenciado" + gameObject);
                break;
            case StepType.ChangeToMercenary:
                if (uiManagerMilitaryBase) uiManagerMilitaryBase.DoExceptFilterGranade();
                else Debug.Log("No existe el objeto referenciado" + gameObject);
                break;
            case StepType.GoBattleToCampaing:
                if (uiManagerCampaign) uiManagerCampaign.GoToMenuGameplay();
                else Debug.Log("No existe el objeto referenciado" + gameObject);
                break;
        }

        OnStepDone?.Invoke();
    }

    void OnEndWriting()
    {
        btnSkip.gameObject.SetActive(false);
        if (hasArrow)
            arrow.gameObject.SetActive(true);
    }
}

public enum StepType
{
    Continue,
    GoCampaignToMilitaryBase,
    BuyUnit,
    BuySlot,
    BuyUpgrade,
    BuyHealth,
    GoMilitaryBaseToCampaing,
    ChangeToGranade,
    ChangeToMercenary,
    GoBattleToCampaing
}