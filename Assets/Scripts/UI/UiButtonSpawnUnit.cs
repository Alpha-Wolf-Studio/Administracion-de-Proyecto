using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UiButtonSpawnUnit : MonoBehaviour, IPointerDownHandler
{
    public MilitaryType militaryType;
    public int idUnit;
    private int troopAmount;
    [SerializeField] private Image overlayImage;
    [SerializeField] private TMP_Text textAmount;
    private Image thisImage;
    private bool isAvailable;
    private float currentCooldown = -1;
    private const float maxCooldown = 0.2f;

    private void Awake ()
    {
        thisImage = GetComponent<Image>();
        overlayImage.sprite = thisImage.sprite;
    }

    public void Set (int amount)
    {
        troopAmount = amount;
        isAvailable = troopAmount > 0;
        textAmount.text = troopAmount.ToString();
     
        if (!isAvailable)
            overlayImage.fillAmount = 1;
    }

    private void Update ()
    {
        if (isAvailable && currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
            overlayImage.fillAmount = currentCooldown / maxCooldown;

            if (currentCooldown < 0)
            {
                overlayImage.fillAmount = 0;
                overlayImage.raycastTarget = false;
            }
        }
    }

    public void OnPointerDown (PointerEventData eventData)
    {
        if (!isAvailable)
            return;
        GamePlayManager.Get().GetPlayerTroopManager().OnButtonCreateTroop(idUnit, militaryType);
        currentCooldown = maxCooldown;
        troopAmount--;
        textAmount.text = troopAmount.ToString();
        overlayImage.raycastTarget = true;

        isAvailable = troopAmount > 0;
        if (!isAvailable)
            enabled = false;
        overlayImage.fillAmount = 1;
    }
}