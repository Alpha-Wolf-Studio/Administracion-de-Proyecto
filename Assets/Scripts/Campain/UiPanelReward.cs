using TMPro;
using UnityEngine;

public class UiPanelReward : MonoBehaviour
{
    [SerializeField] private TMP_Text textReward;
    [SerializeField] private string baseText;

    public void SetText (int amount)
    {
        gameObject.SetActive(amount != 0);
        textReward.text = baseText + "\n" + amount;
    }
}