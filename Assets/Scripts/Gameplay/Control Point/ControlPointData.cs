using UnityEngine;

[System.Serializable]
public class ControlPointData
{
    public LanesFlags controlLanesFlags = default;
    public float controlPointLife = 25f;
    [Header("Control Bonus")]
    [Range(0, 500)] public float controlBonusDamage = 0;
    [Range(0, 500)] public float controlBonusRange = 0;
    
    [Header("Unlock Bonus")]
    [Range(0, 500)] public float unlockBonusDamage = 0;
    [Range(0, 500)] public float unlockBonusRange = 0;
    [Range(0, 500)] public float unlockHealAmount = 0;
    
    public void ResetControlPointBonus()
    {
        controlBonusDamage = 0;
        unlockBonusDamage = 0;
    }
    
    public void SetControlPointBonus(ControlPointData newData)
    {
        controlLanesFlags = newData.controlLanesFlags;
        
        controlBonusDamage = newData.controlBonusDamage;
        controlBonusRange = newData.controlBonusRange;
        
        unlockBonusDamage = newData.unlockBonusDamage;
        unlockBonusRange = newData.unlockBonusRange;
        unlockHealAmount = newData.unlockHealAmount;
    }
    
    public void AddControlPointBonus(ControlPointData newData)
    {
        controlLanesFlags = newData.controlLanesFlags;
        
        controlBonusDamage += newData.controlBonusDamage;
        controlBonusRange += newData.controlBonusRange;
        
        unlockBonusDamage += newData.unlockBonusDamage;
        unlockBonusRange += newData.unlockBonusRange;
        unlockHealAmount += newData.unlockHealAmount;
    }
    
}
