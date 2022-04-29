using UnityEngine;
using UnityEngine.UI;

public class UiButtonSpawnUnit : MonoBehaviour
{
    [SerializeField]private int index;
    private Image image;
    private TroopManager troopManager;

    private void Awake()
    {
        image = GetComponent<Image>();
    }
    public void UpdateValues(int newIndex, TroopManager troopManagerPlayer)
    {
        troopManager = troopManagerPlayer;
        index = newIndex;

        int indexImage = (int) GameManager.Get().unitsStatsLoaded[index].tempCurrentShape;
        image.sprite = GameManager.Get().GetCurrentSprite(indexImage);
        image.color = GameManager.Get().unitsStatsLoaded[index].tempColor;
    }
    public void OnButtonSpawnUnit()
    {
        troopManager.OnButtonCreateTroop(index);
    }
}