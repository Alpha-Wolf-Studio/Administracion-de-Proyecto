using System;

public static class HealthRecoverCalculator
{
    
    public static Action OnUnitsHeal;

    public const int SecondsBetweenHeal = 10;
    public static readonly float HealAmountPerSeconds = 0.0066f;
    
    public static void GetOfflineHealth(PlayerData playerData) 
    {
        DateTimeOffset now = DateTimeOffset.UtcNow;
        long unixTimeMilliseconds = now.ToUnixTimeMilliseconds();
        long timeDifference = unixTimeMilliseconds - playerData.LastSavedTime;
        int secondsDifference = (int)(timeDifference / 1000);
        RecalculateHealth(playerData, secondsDifference);
    }

    public static void RecalculateHealth(PlayerData playerData, int secondsDifference)
    {
        float healAmount = HealAmountPerSeconds * secondsDifference;

        foreach (var unitData in playerData.DataArmies)
        {
            float maxLife = GameManager.Get().GetUnitStats(unitData.IdUnit).GetLifeLevel(GameManager.Get().GetlevelUnit(unitData.IdUnit, MilitaryType.Army), unitData.IdUnit);

            if (unitData.Life < maxLife)
            {
                unitData.Life += healAmount;
                if (unitData.Life > maxLife)
                    unitData.Life = maxLife;
            }
        }

        foreach (var unitData in playerData.DataMercenaries)
        {
            float maxLife = GameManager.Get().GetUnitStats(unitData.IdUnit).GetLifeLevel(GameManager.Get().GetlevelUnit(unitData.IdUnit, MilitaryType.Mercenary), unitData.IdUnit);
            if (unitData.Life < maxLife)
            {
                unitData.Life += healAmount;
                if (unitData.Life > maxLife)
                    unitData.Life = maxLife;
            }
        }
        
        OnUnitsHeal?.Invoke();
    }

}
