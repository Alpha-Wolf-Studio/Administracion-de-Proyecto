using System;

public static class GoldCalculator
{

    public static Action OnIncomeGoldChange;

    private static int _incomeGold;
    public static int IncomeGold => _incomeGold;

    public static readonly int SecondsBetweenIncome = 150;

    public static int GetOfflineGold(WorldBuilderData worldData, PlayerData playerData) 
    {

        DateTimeOffset now = DateTimeOffset.UtcNow;
        long unixTimeMilliseconds = now.ToUnixTimeMilliseconds();
        long timeDifference = unixTimeMilliseconds - playerData.LastSavedTime;
        int secondsDifference = (int)(timeDifference / 1000);

        RecalculateIncomeGold(worldData, playerData);

        int extraGold = IncomeGold * (secondsDifference / SecondsBetweenIncome);
        return extraGold;

    }

    public static void RecalculateIncomeGold(WorldBuilderData worldData, PlayerData playerData) 
    {

        _incomeGold = 0;
        var provinces = worldData.ProvincesData.Provinces;
        int[] provincesTerrainsAmount = new int[provinces.Count];

        for (int i = 0; i < playerData.CampaingStatus.Length; i++)
        {
            if ((TerrainManager.TerrainState)playerData.CampaingStatus[i] == TerrainManager.TerrainState.Unlocked)
            {

                var levelData = worldData.LevelsData.GetLevelDataByFalseIndex(i);
                if (levelData != null)
                {
                    _incomeGold += levelData.GoldIncome;

                    int provinceIndex = levelData.ProvinceIndex;
                    if (provinceIndex < provincesTerrainsAmount.Length)
                    {
                        provincesTerrainsAmount[provinceIndex]++;
                    }
                }

            }
        }

        for (int i = 0; i < provincesTerrainsAmount.Length; i++)
        {
            if (provinces[i].CurrentTerrainsAmount == 0) continue;

            if (provincesTerrainsAmount[i] == provinces[i].CurrentTerrainsAmount)
            {
                _incomeGold += provinces[i].BonusIncomeGold;
            }
        }

        OnIncomeGoldChange?.Invoke();
    }

}
