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

    private float currentCooldown = -1;
    private const float maxCooldown = 0.2f;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    public void Set (int amount)
    {
        troopAmount = amount;
        textAmount.text = troopAmount.ToString();
        ButtonDisableLogic();
    }

    private void Update()
    {
        if(currentCooldown > 0) 
        {
            currentCooldown -= Time.deltaTime;
            overlayImage.fillAmount = currentCooldown / maxCooldown;
            if(currentCooldown < 0) 
            {
                overlayImage.fillAmount = 0;
                overlayImage.raycastTarget = false;
            }
        }
    }

    public void OnPointerDown (PointerEventData eventData)
    {
        GamePlayManager.Get().GetPlayerTroopManager().OnButtonCreateTroop(idUnit, militaryType, troopAmount);
        
        if (troopAmount > 0)
        {
            troopAmount--;
            currentCooldown = maxCooldown;
            textAmount.text = troopAmount.ToString();
            overlayImage.raycastTarget = true;
        }
        
        ButtonDisableLogic();
        
    }

    private void ButtonDisableLogic()
    {
        if (troopAmount <= 0)
        {
            overlayImage.raycastTarget = true;
            textAmount.text = "";
            button.interactable = false;
        }
    }

}