using UnityEngine;
using UnityEngine.UI;

public class UiButtonSpawnUnit : MonoBehaviour
{
    [SerializeField] private int index = 0;
    [SerializeField] private Image buttonImage = default;
    [SerializeField] private Image overlayImage = default;
    private TroopManager troopManager;

    private float currentCooldown = -1;
    private float maxCooldown = 5;

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

    public void UpdateValues(int newIndex, float troopCooldown, TroopManager troopManagerPlayer)
    {
        troopManager = troopManagerPlayer;
        index = newIndex;
        maxCooldown = troopCooldown;

        var sprite = GameManager.Get().GetCurrentSprite(index);
        buttonImage.sprite = sprite;
        overlayImage.sprite = sprite;
    }

    public void OnButtonSpawnUnit()
    {
        if (currentCooldown > 0) return;
        troopManager.OnButtonCreateTroop(index);
        currentCooldown = maxCooldown;
        overlayImage.raycastTarget = false;
    }
}