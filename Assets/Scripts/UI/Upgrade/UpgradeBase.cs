using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UpgradeBase : MonoBehaviour, IPointerClickHandler
{
    public event Action OnUpdateUpgrade;
    public bool isInited;
    private Image image;
    protected UiMilitaryBase uiMilitaryBase;
    [SerializeField] protected TMP_Text textCost;
    protected int cost;
    private Animator animator;
    private static readonly int Successful = Animator.StringToHash("Successful");
    private static readonly int Fail = Animator.StringToHash("Fail");

    private void Awake ()
    {
        image = GetComponent<Image>();
        animator = GetComponent<Animator>();
    }

    public void Initialize (UiMilitaryBase uiMilitaryBase)
    {
        isInited = true;
        this.uiMilitaryBase = uiMilitaryBase;
        UpdateCost();
    }

    public abstract void UpdateCost ();

    public void OnPointerClick (PointerEventData eventData)
    {
        BuyUpgrade();
        OnUpdateUpgrade?.Invoke();
    }

    public void SetImage (Sprite sprite) => image.sprite = sprite;

    protected abstract void BuyUpgrade ();

    protected void SetTryBuy (bool wasSuccessfulBuy)
    {
        if (wasSuccessfulBuy)
        {
            animator.SetTrigger(Successful);
        }
        else
        {
            animator.SetTrigger(Fail);
        }
    }
}