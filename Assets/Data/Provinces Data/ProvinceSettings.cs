using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Province Setting", menuName = "Provinces/Province Settings")]
public class ProvinceSettings : ScriptableObject
{
    public string ProvinceName = "";
    public Color Color = Color.white;
    public int BonusGetGold = 10;
    public int BonusIncomeGold = 10;
    [HideInInspector] public int CurrentTerrainsAmount = 0;
}
