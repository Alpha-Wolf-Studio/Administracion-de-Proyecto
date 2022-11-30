using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UpgradeBase : MonoBehaviour, IPointerClickHandler
{
    public event Action OnUpdateUpgrade;
    private Image image;
    protected UiMilitaryBase uiMilitaryBase;
    [SerializeField] protected TMP_Text textCost;
    protected int cost;
    private Animator animator;

    private void Awake ()
    {
        image = GetComponent<Image>();
        animator = GetComponent<Animator>();
    }

    public void Initialize (UiMilitaryBase uiMilitaryBase)
    {
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
}