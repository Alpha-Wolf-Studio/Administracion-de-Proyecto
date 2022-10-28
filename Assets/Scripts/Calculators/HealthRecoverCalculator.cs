using System;

public static class HealthRecoverCalculator
{
    
    public static Action OnUnitsHeal;

    public const int SecondsBetweenHeal = 1;
    public static readonly float HealAmountPerSeconds = .25f;
    
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
            float maxLife = GameManager.Get().unitsStatsLoaded[unitData.IdUnit].life;
            if (unitData.Life < maxLife)
            {
                unitData.Life += healAmount;
                if (unitData.Life > maxLife)
                    unitData.Life = maxLife;
            }
        }

        foreach (var unitData in playerData.DataMercenaries)
        {
            float maxLife = GameManager.Get().unitsStatsLoaded[unitData.IdUnit].life;
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