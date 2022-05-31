using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiPanelShopStat : MonoBehaviour
{
    public TMP_Text titleName;
    public TMP_Text currentValue;
    public TMP_Text nextValue;
    public Image imageStat;
    public Image fillCurrentValue;
    public Image fillPlusValue;

    public void UpdateUi(float current, float next, float max, int unitLevel)
    {
        currentValue.text = current.ToString("F2");
        nextValue.text = (next - current).ToString("F2");
        fillCurrentValue.fillAmount = current / max;
        fillPlusValue.fillAmount = next / max;
    }
}