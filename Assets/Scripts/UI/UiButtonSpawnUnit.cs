using System.Collections.Generic;
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
    private Button button;
    private List<int> unitsIndexList = new List<int>();

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
        {
            ButtonDisableLogic();
            overlayImage.fillAmount = 1;
        }

        unitsIndexList = militaryType == MilitaryType.Army
            ? GameManager.Get().GetAllArmyUnitsIndexWithType(idUnit)
            : GameManager.Get().GetAllMercenaryUnitsIndexWithType(idUnit);
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
        SpawnUnit();
    }

    public void SpawnUnit ()
    {
        if (!isAvailable)
            return;

        currentCooldown = maxCooldown;

        troopAmount--;
        textAmount.text = troopAmount.ToString();
        overlayImage.raycastTarget = true;

        GamePlayManager.Get().GetPlayerTroopManager().OnButtonCreateTroop(idUnit, unitsIndexList[troopAmount], militaryType);

        isAvailable = troopAmount > 0;
        if (!isAvailable)
        {
            ButtonDisableLogic();
            enabled = false;
        }

        overlayImage.fillAmount = 1;
    }

    private void ButtonDisableLogic ()
    {
        if (troopAmount <= 0)
        {
            overlayImage.raycastTarget = true;
            textAmount.text = "";
        }
    }
}