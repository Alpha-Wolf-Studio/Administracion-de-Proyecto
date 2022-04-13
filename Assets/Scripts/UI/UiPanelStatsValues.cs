using TMPro;
using UnityEngine;

public class UiPanelStatsValues : MonoBehaviour
{
    [SerializeField] private string nameBase = "";
    [SerializeField] private TextMeshProUGUI textTitles;
    [SerializeField] private int value;

    void Start()
    {
        SetValue(value);
    }
    public void UpdateUI()
    {
        textTitles.text = nameBase + value;
    }
    public void ModifyValue(bool isIncrement)
    {
        int increment = isIncrement ? 1 : -1;

        if (Input.GetKey(KeyCode.LeftShift))
            increment *= 5;
        if (Input.GetKey(KeyCode.LeftControl))
            increment *= 10;
        
        value += increment;
        if (value < 0) value = 0;
        SetValue(value);
    }
    public int GetValue()
    {
        return value;
    }
    public void SetValue(int value)
    {
        this.value = value;
        UpdateUI();
    }
}