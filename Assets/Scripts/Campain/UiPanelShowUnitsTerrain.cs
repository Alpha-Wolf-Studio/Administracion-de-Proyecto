using TMPro;
using UnityEngine;

public class UiPanelShowUnitsTerrain : MonoBehaviour
{
    [SerializeField] private TMP_Text textUnitCount;

    public void Set (int amount)
    {
        gameObject.SetActive(amount != 0);

        string textAmount = (amount < 10 ? " " : "") + amount.ToString("F0");
        textUnitCount.text = textAmount;
    }
}