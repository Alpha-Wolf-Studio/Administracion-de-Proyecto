using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UpgradeBase : MonoBehaviour, IPointerClickHandler
{
    public event Action OnUpdateUpgrade;
    private Image image;
    protected UiMilitaryBase uiMilitaryBase;

    private void Awake ()
    {
        image = GetComponent<Image>();
    }

    public void Initialize (UiMilitaryBase uiMilitaryBase)
    {
        this.uiMilitaryBase = uiMilitaryBase;
    }

    public void OnPointerClick (PointerEventData eventData)
    {
        BuyUpgrade();
        OnUpdateUpgrade?.Invoke();
    }

    public void SetImage (Sprite sprite) => image.sprite = sprite;

    protected abstract void BuyUpgrade ();
}