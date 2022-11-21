using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Unit Upgrades File", menuName = "Upgrades/Unit Upgrades File", order = 1)]
public class UnitUpgradeScriptableObject : ScriptableObject
{

    public void SetUpgradesInstance() => Instance = this;
    public static UnitUpgradeScriptableObject Instance { get; private set; }

    public List<UnitUpgrade> upgrades = new List<UnitUpgrade>(); 
    
    [System.Serializable]
    public class UnitUpgrade
    {
        public string UnitName;
        public int UnitID;
        public List<UpgradeByLevel> UpgradeByLevel;
    }
    
    [System.Serializable]
    public class UpgradeByLevel
    {
        public float LifeAmount;
        public float DamageAmount;
    }
    
    public float Life (int idUnit, int level)
    {
        int index = upgrades.FindIndex(i => i.UnitID == idUnit);

        if (level >= upgrades[index].UpgradeByLevel.Count)
            level = upgrades[index].UpgradeByLevel.Count - 1;
        
        return upgrades[index].UpgradeByLevel[level].LifeAmount;
    }

    public float Damage (int idUnit, int level)
    {
        int index = upgrades.FindIndex(i => i.UnitID == idUnit);
        
        if (level >= upgrades[index].UpgradeByLevel.Count)
            level = upgrades[index].UpgradeByLevel.Count - 1;
        
        return upgrades[index].UpgradeByLevel[level].DamageAmount;
    }
    
}
