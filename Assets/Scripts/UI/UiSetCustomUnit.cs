using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UiSetCustomUnit : MonoBehaviourSingleton<UiSetCustomUnit>
{
    public Action<int> OnNewUnitSaved;
    public int index = 0;
    [SerializeField] private UnitStats unitStats;

    [Header("References: ")] 
    [SerializeField] private TMP_InputField nameField;
    [SerializeField] private TMP_Dropdown unitType;
    [SerializeField] private TMP_Dropdown movementType;
    [SerializeField] private TMP_Dropdown attackType;
    [SerializeField] private List<Toggle> unitsDamageables;
    [SerializeField] private List<Toggle> unitsPlusDamage;
    [SerializeField] private List<Toggle> unitsRestDamage;
    [SerializeField] private UiPanelStatsValues lifePanel;
    [SerializeField] private UiPanelStatsValues damagePanel;
    [SerializeField] private UiPanelStatsValues slightRangePanel;
    [SerializeField] private UiPanelStatsValues attackRangePanel;
    [SerializeField] private UiPanelStatsValues velocity;
    [SerializeField] private UiPanelStatsValues shootRatePanel;
    [SerializeField] private UiUnitVisualSettings visualSettings;

    public void OverwriteUnit()
    {
        if (index < 0) return;
        SaveValues(index);
    }
    public void SaveValues(int newIndex = -1)
    {
        unitStats = new UnitStats();
        unitStats.nameUnit = nameField.text;
        unitStats.unitsDamageables = new List<UnitsType>();

        unitStats.movementType = (MovementType) movementType.value;
        unitStats.attackType = (AttackType) attackType.value;
        unitStats.unitType = (UnitsType) unitType.value;

        for (int i = 0; i < unitsDamageables.Count; i++)
        {
            if (unitsDamageables[i].isOn)
                unitStats.unitsDamageables.Add((UnitsType)i);
        }

        unitStats.unitsPlusDamage = new List<UnitsType>();
        for (int i = 0; i < unitsPlusDamage.Count; i++)
        {
            if (unitsPlusDamage[i].isOn)
                unitStats.unitsPlusDamage.Add((UnitsType)i);
        }

        unitStats.unitsRestDamage = new List<UnitsType>();
        for (int i = 0; i < unitsRestDamage.Count; i++)
        {
            if (unitsRestDamage[i].isOn)
                unitStats.unitsRestDamage.Add((UnitsType)i);
        }

        unitStats.life = lifePanel.GetValue();
        unitStats.damage = damagePanel.GetValue();
        unitStats.radiusSight = slightRangePanel.GetValue();
        unitStats.rangeAttack = attackRangePanel.GetValue();
        unitStats.velocity = velocity.GetValue();
        unitStats.fireRate = shootRatePanel.GetValue();

        unitStats.tempColor = visualSettings.GetColor();
        unitStats.tempCurrentShape = visualSettings.GetCurrentShape();

        if (newIndex < 0)
        {
            newIndex = GameManager.Get().unitsStatsLoaded.Count;
            GameManager.Get().unitsStatsLoaded.Add(unitStats);
            OnNewUnitSaved?.Invoke(newIndex);
            index = newIndex;
        }
        else
        {
            GameManager.Get().unitsStatsLoaded[newIndex] = unitStats;
        }
        string data = JsonUtility.ToJson(unitStats, true);
        LoadAndSave.SaveToFile(GameManager.unitsStatsPath + newIndex, data);
    }

    public void LoadValues(int newIndex)
    {
        index = newIndex;
        unitStats = GameManager.Get().GetUnitStats(index);

        nameField.text = unitStats.nameUnit;
        unitType.value = (int) unitStats.unitType;
        movementType.value = (int) unitStats.movementType;
        attackType.value = (int) unitStats.attackType;

        for (int i = 0; i < unitsDamageables.Count; i++)
            unitsDamageables[i].isOn = false;
        for (int i = 0; i < unitStats.unitsDamageables.Count; i++)
        {
            unitsDamageables[(int)unitStats.unitsDamageables[i]].isOn = true;
        }

        for (int i = 0; i < unitsPlusDamage.Count; i++)
            unitsPlusDamage[i].isOn = false;
        for (int i = 0; i < unitStats.unitsPlusDamage.Count; i++)
        {
            unitsPlusDamage[(int)unitStats.unitsPlusDamage[i]].isOn = true;
        }

        for (int i = 0; i < unitsRestDamage.Count; i++)
            unitsRestDamage[i].isOn = false;
        for (int i = 0; i < unitStats.unitsRestDamage.Count; i++)
        {
            unitsRestDamage[(int)unitStats.unitsRestDamage[i]].isOn = true;
        }

        lifePanel.SetValue((int) unitStats.life);
        damagePanel.SetValue((int) unitStats.damage);
        slightRangePanel.SetValue((int) unitStats.radiusSight);
        attackRangePanel.SetValue((int) unitStats.rangeAttack);
        velocity.SetValue((int) unitStats.velocity);
        shootRatePanel.SetValue((int) unitStats.fireRate);
        visualSettings.SetColor(unitStats.tempColor);
        visualSettings.SetMesh(unitStats.tempCurrentShape);
    }
}