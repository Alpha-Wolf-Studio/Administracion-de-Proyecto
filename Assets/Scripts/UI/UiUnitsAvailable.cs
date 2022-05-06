using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiUnitsAvailable : MonoBehaviour
{
    [SerializeField] private GameObject pfButton;
    private List<GameObject> buttonsLoaded = new List<GameObject>();

    private void Awake()
    {
        UiSetCustomUnit.Get().OnNewUnitSaved += AddNewButton;
    }
    void Start()
    {
        LoadButtons();
    }
    void LoadButtons()
    {
        buttonsLoaded.Clear();
        for (int i = 0; i < GameManager.Get().unitsStatsLoaded.Count; i++)
        {
            GameObject go = Instantiate(pfButton, Vector3.zero, Quaternion.identity, transform);
            buttonsLoaded.Add(go);

            int indexShape = (int) GameManager.Get().unitsStatsLoaded[i].tempCurrentShape;
            Image goImage= go.GetComponent<Image>();
            goImage.sprite = GameManager.Get().GetCurrentSprite(indexShape);
            goImage.color = GameManager.Get().unitsStatsLoaded[i].tempColor;

            UiButtonCreatorUnit buttonCreatorUnit = go.GetComponent<UiButtonCreatorUnit>();
            buttonCreatorUnit.index = i;
        }
    }

    void AddNewButton(int i)
    {
        GameObject go = Instantiate(pfButton, Vector3.zero, Quaternion.identity, transform);
        buttonsLoaded.Add(go);

        int indexShape = (int)GameManager.Get().unitsStatsLoaded[i].tempCurrentShape;
        Image goImage = go.GetComponent<Image>();
        goImage.sprite = GameManager.Get().GetCurrentSprite(indexShape);
        goImage.color = GameManager.Get().unitsStatsLoaded[i].tempColor;

        UiButtonCreatorUnit buttonCreatorUnit = go.GetComponent<UiButtonCreatorUnit>();
        buttonCreatorUnit.index = i;
    }
}