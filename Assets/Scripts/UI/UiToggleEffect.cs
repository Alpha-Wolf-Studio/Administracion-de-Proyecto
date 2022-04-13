using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiToggleEffect : MonoBehaviour
{
    public bool isOn;

    [Header("References: ")]
    [SerializeField] private TextMeshProUGUI textInToggle;
    [SerializeField] private Image imageInToggle;

    [Header("Settings: ")]
    [SerializeField] private Color colorEnable = Color.green;
    [SerializeField] private Color colorDisable = Color.red;

    private Toggle toggle;
    public Action OnValueChangeAction;

    private void Awake()
    {
        toggle = GetComponent<Toggle>();
    }
    private void Start()
    {
        OnValueChange(toggle.isOn);
    }
    public void OnValueChange(bool isOn)
    {
        this.isOn = isOn;
        textInToggle.color = isOn ? colorEnable : colorDisable;
        imageInToggle.color = isOn ? colorEnable : colorDisable;
        OnValueChangeAction?.Invoke();
    }
}