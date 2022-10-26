using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiPanelProfile : MonoBehaviour
{
    [SerializeField] private List<TMP_Text> textPlayerName = new List<TMP_Text>();
    [SerializeField] private TMP_Text textlevel;
    [SerializeField] private TMP_InputField inputFieldName;
    [SerializeField] private Button btnConfirm;

    private void Awake ()
    {
        ChangePlayerName(GameManager.Get().GetPlayerName());
    }

    private void Start ()
    {
        inputFieldName.text = GameManager.Get().GetPlayerName();
        inputFieldName.onValueChanged.AddListener(ChangePlayerName);
        btnConfirm.onClick.AddListener(ButtonConfirm);

        int level = GameManager.Get().GetLastLevelPlayer();
        if (level < 10)
            textlevel.text = "00" + level;
        else if (level < 100)
            textlevel.text = "0" + level;
        else
            textlevel.text = level.ToString();
    }

    private void ChangePlayerName (string newName)
    {
        foreach (TMP_Text textName in textPlayerName)
        {
            textName.text = newName;
        }
    }

    private void ButtonConfirm ()
    {
        GameManager.Get().SetPlayerDataName(inputFieldName.text);
        gameObject.SetActive(false);
    }
}