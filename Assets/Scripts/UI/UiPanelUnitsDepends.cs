using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiPanelUnitsDepends : MonoBehaviour
{
    [SerializeField] private UiToggleEffect[] togglesUnits;
    [SerializeField] private Toggle[] myTogglesUnits;

    void Start()
    {
        for (var i = 0; i < togglesUnits.Length; i++)
        {
            togglesUnits[i].OnValueChangeAction += UpdateAvailableList;
        }
    }
    void UpdateAvailableList()
    {
        for (var i = 0; i < togglesUnits.Length; i++)
        {
            myTogglesUnits[i].interactable = togglesUnits[i].isOn;
        }
    }
}