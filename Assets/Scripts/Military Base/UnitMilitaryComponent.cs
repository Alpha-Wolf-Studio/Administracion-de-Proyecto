using System;
using UnityEngine;
using UnityEngine.UI;

public class UnitMilitaryComponent : MonoBehaviour
{
    public Image imageUnit;
    public Image imageFillLife;
    public GameObject imageContentLife;

    private void Start ()
    {
        GameManager.Get().OnHealtAllUnits += SetFullLife;
    }

    private void SetFullLife ()
    {
        imageFillLife.fillAmount = 1;
    }

    public void ShowLifeBar (bool isEnable)
    {
        imageContentLife.SetActive(isEnable);
    }

    public void UpdateFillLife (int maxLife, float currentLife)
    {
        imageContentLife.SetActive(true);
        imageFillLife.fillAmount = currentLife / maxLife;
    }
}