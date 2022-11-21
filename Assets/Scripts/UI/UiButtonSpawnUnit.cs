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

    public void Set (int amount)
    {
        troopAmount = amount;
        textAmount.text = troopAmount.ToString();
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
        GamePlayManager.Get().GetPlayerTroopManager().OnButtonCreateTroop(idUnit, militaryType);
        currentCooldown = maxCooldown;
        troopAmount--;
        textAmount.text = troopAmount.ToString();
        overlayImage.raycastTarget = true;
    }
}