using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Provinces Data", menuName = "Provinces/All Provinces Data")]
public class ProvincesData : ScriptableObject
{
    public List<ProvinceSettings> Provinces = default;

    public void SaveProvinceData(List<LevelData> allLevelsData) 
    {
        foreach (var province in Provinces)
        {
            province.CurrentTerrainsAmount = 0;
        }

        foreach (var data in allLevelsData)
        {
            if(Provinces[data.ProvinceIndex] != null) 
            {
                Provinces[data.ProvinceIndex].CurrentTerrainsAmount++;
            }
        }
    }

}