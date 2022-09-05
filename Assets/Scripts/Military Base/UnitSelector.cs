using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSelector : MonoBehaviour
{
    [SerializeField] private Button pfButtonUnitSelector;
    [SerializeField] private Transform content;
    [SerializeField] private List<Button> btnUnitsSelector = new List<Button>();

    public int currentUnit;

    void Start ()
    {
        foreach (Button button in btnUnitsSelector)
        {
            button.gameObject.SetActive(false);
        }

        for (int i = 0; i < GameManager.Get().unitsStatsLoaded.Count; i++)
        {
            int index = i;
            if (index >= btnUnitsSelector.Count)
            {
                Button btn = Instantiate(pfButtonUnitSelector, content);
                btnUnitsSelector.Add(btn);
            }

            InizializeButton(index);
        }

        SetCurrentUnit(0);
    }

    void InizializeButton (int index)
    {
        btnUnitsSelector[index].onClick.AddListener(() => SetCurrentUnit(index));
        btnUnitsSelector[index].gameObject.SetActive(true);
        btnUnitsSelector[index].image.sprite = GameManager.Get().GetCurrentSprite(index);
    }

    void SetCurrentUnit (int index)
    {
        btnUnitsSelector[currentUnit].image.color = Color.white;
        currentUnit = index;
        btnUnitsSelector[currentUnit].image.color = Color.green;
    }
}