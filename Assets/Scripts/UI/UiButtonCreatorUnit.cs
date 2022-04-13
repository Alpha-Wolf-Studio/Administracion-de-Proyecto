using UnityEngine;
using UnityEngine.UI;

public class UiButtonCreatorUnit : MonoBehaviour
{
    public int index = 0;
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Start()
    {
        UiUnitVisualSettings.Get().OnUpdateUI += UpdateUI;
    }
    public void LoadUnit()
    {
        UiSetCustomUnit.Get().LoadValues(index);
    }
    void UpdateUI()
    {
        if (UiSetCustomUnit.Get().index == index)
        {
            image.color = UiUnitVisualSettings.Get().GetColor();

            int indexSprite = (int) UiUnitVisualSettings.Get().GetCurrentShape();
            image.sprite = GameManager.Get().GetCurrentSprite(indexSprite);
        }
        else
        {
            image.color = GameManager.Get().unitsStatsLoaded[index].tempColor;

            int indexSprite = (int)GameManager.Get().unitsStatsLoaded[index].tempCurrentShape;
            image.sprite = GameManager.Get().GetCurrentSprite(indexSprite);
        }
    }
}