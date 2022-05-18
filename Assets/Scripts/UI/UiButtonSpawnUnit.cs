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
        image.sprite = GameManager.Get().GetCurrentSprite(index);
    }
    public void OnButtonSpawnUnit()
    {
        troopManager.OnButtonCreateTroop(index);
    }
}