using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public Action onLoadedStats;
    public event Action OnHealtAllUnits;
    public event Action OnAddLevelUnit;
    public event Action OnAddUnitArmy;
    public event Action OnAddUnitMercenary;
    public static string unitsStatsPath = "UnitStat";
    public List<UnitStats> unitsStatsLoaded = new List<UnitStats>();
    [SerializeField] private Mesh[] meshes;
    [SerializeField] private List<SpritesPerArmy> spritesArmy = new List<SpritesPerArmy>();
    [SerializeField] private List<Sprite> spritesCurrencyTypes = new List<Sprite>();
    [Space(10)]
    [SerializeField] private WorldBuilderData worldData = default;
    [SerializeField] private UnitUpgradeScriptableObject unitUpgrades;

    private string pathPlayerData = "PlayerData";
    [SerializeField] private PlayerData playerData;
    public LevelData CurrentSelectedLevel = new LevelData();
    [SerializeField] private GameObject prefabTutorial;
    private GameObject instanceTutorial;

    public override void Awake ()
    {
        base.Awake();
        
        #if UNITY_STANDALONE
        
        Screen.SetResolution(960, 540, false);
        
        #endif
        
        LoadAllStatsSaved();
        LoadAllPlayerData();
    }

    private void Start ()
    {
        Application.quitting += SavePlayerData;
        StartCoroutine(IncomeExtraGoldCoroutine());
        StartCoroutine(HealUnitsCoroutine());
        Time.timeScale = 1;

        if (!playerData.doneTutorial)
        {
            instanceTutorial = Instantiate(prefabTutorial);
            instanceTutorial.GetComponent<TutorialManager>().StartTutorial(0, 0);
        }
    }

    private void OnDestroy() // Todo: Probar que esto funcione cuando android te cierra el proceso
    {
        SavePlayerData();
    }

    private void LoadAllPlayerData() 
    {
        playerData = JsonUtility.FromJson<PlayerData>(LoadAndSave.LoadFromFile(pathPlayerData, true));
        if (playerData == null)
        {
            ResetPlayerData();
            SavePlayerData();
        }
        
        playerData.CampaingStatus ??= TerrainManager.GetDefaultTerrainEnumIndexes(worldData.Rows, worldData.Columns);
        CurrentSelectedLevel = worldData.LevelsData.GetLevelData(playerData.LastLevelComplete);
    }

    void LoadAllStatsSaved()
    {
        unitUpgrades.SetUpgradesInstance();
        
        bool noMoreTexts = false;
        int index = 0;

        unitsStatsLoaded.Clear();
        while (!noMoreTexts)
        {
            string data = LoadAndSave.LoadFromFile(unitsStatsPath + index);
            if (data != null)
            {
                UnitStats stats = JsonUtility.FromJson<UnitStats>(data);
                unitsStatsLoaded.Add(stats);
                index++;
            }
            else
            {
                noMoreTexts = true;
            }
        }

        onLoadedStats?.Invoke();
    }

    /// <summary>
    /// Dinero que se suma o se resta.
    /// </summary>
    /// <param name="plusMoney"></param>
    /// <returns>Si se pudo restar el dinero. Lo resta y guarda en caso de que si</returns>
    public bool ModifyGoldPlayer (int plusMoney)
    {
        if (playerData.CurrentGold + plusMoney < 0)
            return false;
        playerData.CurrentGold += plusMoney;
        SavePlayerData();
        return true;
    }

    public int GetPlayerGold() => playerData.CurrentGold;
    
    public bool ModifyDiamondPlayer (int plusMoney)
    {
        if (playerData.CurrentDiamond + plusMoney < 0)
            return false;
        playerData.CurrentDiamond += plusMoney;
        SavePlayerData();
        return true;
    }

    public int GetPlayerDiamond() => playerData.CurrentDiamond;

    public void SetPlayerDataName (string newName)
    {
        playerData.PlayerName = newName;
        SavePlayerData();
    }

    
    public bool GetPlayerGameCompleteState() => playerData.gameComplete;
    public void SetPlayerGameCompleteState(bool state) => playerData.gameComplete = state;

    public void SetTerrainStates (List<HexagonTerrain> hexagonTerrains)
    {
        playerData.CampaingStatus = new int[hexagonTerrains.Count];
        for (int i = 0; i < hexagonTerrains.Count; i++)
        {
            playerData.CampaingStatus[i] = (int)hexagonTerrains[i].CurrentState;
        }

        SavePlayerData();
    }

    public int[] GetTerrainStates() => playerData.CampaingStatus;

    public LevelData GetLevelDataByFalseIndex(int index) => worldData.LevelsData.GetLevelDataByFalseIndex(index);

    public void SaveLevelEnemiesData()
    {
#if UNITY_EDITOR
        EditorUtility.SetDirty(worldData.LevelsData);
#endif
    }

    public void SaveLevelData(LevelData data)
    {
        worldData.LevelsData.AddTerrainData(data);
#if UNITY_EDITOR
        EditorUtility.SetDirty(worldData.LevelsData);
#endif
    }

    public void SaveProvinceData(List<LevelData> data) 
    {
        worldData.ProvincesData.SaveProvinceData(data);
    }

    public ProvinceSettings GetProvinceSetting(int index) => worldData.ProvincesData.Provinces[index];

    public void TriggerIncomeGoldRecalculation() => GoldCalculator.RecalculateIncomeGold(worldData, playerData);

    private IEnumerator IncomeExtraGoldCoroutine() 
    {

        int incomeTerraingGold = GoldCalculator.GetOfflineGold(worldData, playerData);
        playerData.CurrentGold += incomeTerraingGold;

        while (gameObject) 
        {
            yield return new WaitForSecondsRealtime(GoldCalculator.SecondsBetweenIncome);
            playerData.CurrentGold += GoldCalculator.IncomeGold;
        }
    }
    
    private IEnumerator HealUnitsCoroutine() 
    {
        HealthRecoverCalculator.GetOfflineHealth(playerData);
        while (gameObject) 
        {
            yield return new WaitForSecondsRealtime(HealthRecoverCalculator.SecondsBetweenHeal);
            HealthRecoverCalculator.RecalculateHealth(playerData, HealthRecoverCalculator.SecondsBetweenHeal);
        }
    }

    public int StartingLevelIndex => worldData.startingLevelIndex;
    
    public void OnLevelWin (int level)
    {
        playerData.LastLevelComplete = level - worldData.startingLevelIndex;
        playerData.CurrentGold += worldData.LevelsData.GetLevelDataByFalseIndex(level).GoldOnComplete;
        playerData.CurrentDiamond += worldData.LevelsData.GetLevelDataByFalseIndex(level).DiamondOnComplete;
        

        int[,] twoDCampaingStatusArray = new int[worldData.Rows, worldData.Columns];
        for (int i = 0; i < worldData.Rows; i++)
        {
            for (int j = 0; j < worldData.Columns; j++)
            {
                twoDCampaingStatusArray[i, j] = playerData.CampaingStatus[i * worldData.Columns + j];
            }
        }

        int levelX = level / worldData.Columns;
        int levelY = level % worldData.Columns;

        for (int i = levelX - 1; i <= levelX + 1; i++)
        {
            for (int j = levelY - 1; j <= levelY + 1; j++)
            {

                bool isPositionValid = i >= 0 && i < worldData.Columns && j >= 0 && j < worldData.Rows;
                bool isPositionNeighbours = IsPositionNeighbourHexagonal(i, j, levelX, levelY);

                if (isPositionValid && isPositionNeighbours)
                {

                    if (twoDCampaingStatusArray[i, j] == (int) TerrainManager.TerrainState.Unavailable)
                    {
                        twoDCampaingStatusArray[i, j] = (int) TerrainManager.TerrainState.Locked;
                    }
                }
            }
        }

        twoDCampaingStatusArray[levelX, levelY] = (int) TerrainManager.TerrainState.Unlocked;

        int index = 0;
        for (int i = 0; i <= twoDCampaingStatusArray.GetUpperBound(0); i++)
        {
            for (int z = 0; z <= twoDCampaingStatusArray.GetUpperBound(1); z++)
            {
                playerData.CampaingStatus[index++] = twoDCampaingStatusArray[i, z];
            }
        }

        GoldCalculator.RecalculateIncomeGold(worldData, playerData);

        SavePlayerData();
    }

    public void SetPostLevelUnits(List<KeyValuePair<int, Unit>> armyUnits, List<KeyValuePair<int, Unit>> mercenaryUnits)
    {
        
        foreach (var unitKeyValuePair in armyUnits)
        {
            if (unitKeyValuePair.Value != null && unitKeyValuePair.Value.stats.life > 0)
            {
                float newLifeAmount = unitKeyValuePair.Value.stats.life;
                if (newLifeAmount > 0)
                {
                    playerData.DataArmies[unitKeyValuePair.Key].Life = newLifeAmount;
                }
            }
        }
        
        foreach (var unitKeyValuePair in mercenaryUnits)
        {
            if (unitKeyValuePair.Value != null && unitKeyValuePair.Value.stats.life > 0)
            {
                float newLifeAmount = unitKeyValuePair.Value.stats.life;
                if (newLifeAmount > 0)
                {
                    playerData.DataMercenaries[unitKeyValuePair.Key].Life = newLifeAmount;
                }
            }
        }

        List<int> indexToRemoveList = new List<int>();
        List<UnitData> temporalList = new List<UnitData>(playerData.DataArmies);
        
        foreach (var unitKeyValuePair in armyUnits)
        {
            if (unitKeyValuePair.Value == null || unitKeyValuePair.Value.stats.life <= 0)
            {
                indexToRemoveList.Add(unitKeyValuePair.Key);
            }
        }
        temporalList.RemoveAllIndices(indexToRemoveList);
        playerData.DataArmies = temporalList.ToArray();

        indexToRemoveList.Clear();
        temporalList.Clear();
        temporalList = new List<UnitData>(playerData.DataMercenaries);
        foreach (var unitKeyValuePair in mercenaryUnits)
        {
            if (unitKeyValuePair.Value == null || unitKeyValuePair.Value.stats.life <= 0)
            {
                indexToRemoveList.Add(unitKeyValuePair.Key);
            }
        }
        temporalList.RemoveAllIndices(indexToRemoveList);
        playerData.DataMercenaries = temporalList.ToArray();

    }
    
    private bool IsPositionNeighbourHexagonal (int i, int j, int levelX, int levelY)
    {

        if (levelX % 2 != 0)
        {
            if (j >= levelY + 1 && i != levelX) return false;
        }
        else
        {
            if (j <= levelY - 1 && i != levelX) return false;
        }

        return true;
    }

    public string GetPlayerName() => playerData.PlayerName;
    public int[] GetLevelUnitsArmyPlayer() => playerData.LevelUnitsArmy;
    public float GetLifeUnitsArmyPlayer(int unitIndex) => playerData.DataArmies[unitIndex].Life;
    public int[] GetLevelUnitsMercenaryPlayer() => playerData.LevelUnitsMercenary;
    public float GetLifeUnitsMercenaryPlayer(int unitIndex) => playerData.DataMercenaries[unitIndex].Life;
    public int GetLastLevelPlayer() => playerData.LastLevelComplete;
    public int SetLastLevelPlayer(int level) => playerData.LastLevelComplete = level;
    
    private void SavePlayerData ()
    {
        DateTimeOffset now = DateTimeOffset.UtcNow;
        long unixTimeMilliseconds = now.ToUnixTimeMilliseconds();
        playerData.LastSavedTime = unixTimeMilliseconds;

        bool playerPref = false;
        #if UNITY_ANDROID && !UNITY_EDITOR
        playerPref = true;
        #endif
        
        LoadAndSave.SaveToFile(pathPlayerData, JsonUtility.ToJson(playerData, true), playerPref);
    }

    public UnitStats GetUnitStats(int index) => unitsStatsLoaded[index];
    public int GetLevelsUnitsArmy(int i) => playerData.LevelUnitsArmy[i];
    public int GetLevelsUnitsMercenary(int i) => playerData.LevelUnitsMercenary[i];
    
    public Mesh GetCurrentMesh(int index) => meshes[index];
    public Sprite GetCurrentSprite (int idUnit, MilitaryType militaryType) => spritesArmy[(int) militaryType].sprites[idUnit];
    public int GetLastLevelCompleted() => playerData.LastLevelComplete;
    public bool IsLastLevelCampaignEnd()
    {
        var levelsList = worldData.LevelsData.levelsList;
        return playerData.LastLevelComplete == levelsList[levelsList.Count - 1].Index - worldData.startingLevelIndex;
    }

    public List<int> GetAllArmyUnitsIndexWithType(int typeIndex)
    {
        List<int> mercenaryIndex = new List<int>();
        
        for (int i = 0; i < playerData.DataArmies.Length; i++)
        {
            if(playerData.DataArmies[i].IdUnit == typeIndex)
                mercenaryIndex.Add(i);    
        }

        return mercenaryIndex;
    }
    
    public List<int> GetAllMercenaryUnitsIndexWithType(int typeIndex)
    {
        List<int> mercenaryIndex = new List<int>();
        
        for (int i = 0; i < playerData.DataMercenaries.Length; i++)
        {
            if(playerData.DataMercenaries[i].IdUnit == typeIndex)
                mercenaryIndex.Add(i);    
        }
        
        return mercenaryIndex;
    }
    
    public bool HealAllUnitsFiltered (int cost,CurrencyType currencyType, List<UnitData> unitsFiltered, MilitaryType militaryType)
    {
        switch (currencyType)
        {
            case CurrencyType.Gold:
                if (cost > playerData.CurrentGold)
                    return false;
                playerData.CurrentGold -= cost;
                break;
            case CurrencyType.Diamond:
                if (cost > playerData.CurrentDiamond)
                    return false;
                playerData.CurrentDiamond -= cost;
                break;
        }

        if (unitsFiltered != null && unitsFiltered.Count > 0)
        {
            int idUnit = unitsFiltered[0].IdUnit;
            float maxLife = GameManager.Get().GetUnitStats(idUnit).GetLifeLevel(GameManager.Get().GetlevelUnit(idUnit, militaryType), idUnit);
            for (int i = 0; i < unitsFiltered.Count; i++)
            {
                unitsFiltered[i].Life = maxLife;
            }

            SavePlayerData();
            OnHealtAllUnits?.Invoke();
        }

        return true;
    }

    public bool BuyArmy (int cost,CurrencyType currencyType, int idUnit, MilitaryType militaryType)
    {
        switch (currencyType)
        {
            case CurrencyType.Gold:
                if (cost > playerData.CurrentGold)
                    return false;
                playerData.CurrentGold -= cost;
                break;
            case CurrencyType.Diamond:
                if (cost > playerData.CurrentDiamond)
                    return false;
                playerData.CurrentDiamond -= cost;
                break;
        }

        UnitData[] newUnitData = new UnitData[playerData.DataArmies.Length + 1];
        float maxLife = GameManager.Get().GetUnitStats(idUnit).GetLifeLevel(GameManager.Get().GetlevelUnit(idUnit, militaryType), idUnit);

        for (int i = 0; i < playerData.DataArmies.Length; i++)
        {
            newUnitData[i] = playerData.DataArmies[i];
        }
        newUnitData[newUnitData.Length - 1] = new UnitData(idUnit, unitsStatsLoaded[idUnit].unitType, maxLife);

        playerData.DataArmies = newUnitData;
        SavePlayerData();
        OnAddUnitArmy?.Invoke();

        return true;
    }

    public bool BuyMercenary (int cost,CurrencyType currencyType, int idUnit, MilitaryType militaryType)
    {
        switch (currencyType)
        {
            case CurrencyType.Gold:
                if (cost > playerData.CurrentGold)
                    return false;
                playerData.CurrentGold -= cost;
                break;
            case CurrencyType.Diamond:
                if (cost > playerData.CurrentDiamond)
                    return false;
                playerData.CurrentDiamond -= cost;
                break;
        }

        UnitData[] newUnitData = new UnitData[playerData.DataMercenaries.Length + 1];
        float maxLife = GameManager.Get().GetUnitStats(idUnit).GetLifeLevel(GameManager.Get().GetlevelUnit(idUnit, militaryType), idUnit);

        for (int i = 0; i < playerData.DataMercenaries.Length; i++)
        {
            newUnitData[i] = playerData.DataMercenaries[i];
        }

        newUnitData[newUnitData.Length - 1] = new UnitData(idUnit, unitsStatsLoaded[idUnit].unitType, maxLife);

        playerData.DataMercenaries = newUnitData;
        SavePlayerData();
        OnAddUnitMercenary?.Invoke();
        return true;
    }

    public bool LevelUpUnit (int cost, CurrencyType currencyType, int idUnit, MilitaryType militaryType)
    {
        switch (currencyType)
        {
            case CurrencyType.Gold:
                if (cost > playerData.CurrentGold)
                    return false;
                playerData.CurrentGold -= cost;
                break;
            case CurrencyType.Diamond:
                if (cost > playerData.CurrentDiamond)
                    return false;
                playerData.CurrentDiamond -= cost;
                break;
        }

        switch (militaryType)
        {
            case MilitaryType.Army:
                playerData.LevelUnitsArmy[idUnit]++;
                playerData.LevelUnitsMercenary[idUnit]++;
                break;
            case MilitaryType.Mercenary:
                // Don't use
                break;
        }

        SavePlayerData();
        OnAddLevelUnit?.Invoke();
        return true;
    }

    public UnitData[] GetUnitsArmy () => playerData.DataArmies;
    public UnitData[] GetUnitsMercenary() => playerData.DataMercenaries;

    public bool BuySlot (int cost,CurrencyType currencyType, int idUnit, MilitaryType militaryType)
    {
        switch (currencyType)
        {
            case CurrencyType.Gold:
                if (cost > playerData.CurrentGold)
                    return false;
                playerData.CurrentGold -= cost;
                break;
            case CurrencyType.Diamond:
                if (cost > playerData.CurrentDiamond)
                    return false;
                playerData.CurrentDiamond -= cost;
                break;
        }

        switch (militaryType)
        {
            case MilitaryType.Army:
                playerData.maxAmountArmy[idUnit]++;
                break;
            case MilitaryType.Mercenary:
                playerData.maxAmountMercenary[idUnit]++;
                break;
            default:
                Debug.LogWarning("No existe este Military Type!");
                return false;
        }

        SavePlayerData();
        return true;
    }

    public int GetMaxUnits (int idUnit, MilitaryType militaryType)
    {
        switch (militaryType)
        {
            case MilitaryType.Army:
                return playerData.maxAmountArmy[idUnit];
            case MilitaryType.Mercenary:
                return playerData.maxAmountMercenary[idUnit];
            default:
                return 0;
        }
    }

    public int GetlevelUnit (int idUnit, MilitaryType militaryType)
    {
        switch (militaryType)
        {
            case MilitaryType.Army:
                return playerData.LevelUnitsArmy[idUnit];
            case MilitaryType.Mercenary:
                return playerData.LevelUnitsMercenary[idUnit];
            default:
                return 0;
        }
    }

    public void ResetPlayerData()
    {
        playerData = new PlayerData();
        playerData.CampaingStatus = new int[worldData.Columns * worldData.Rows];
        playerData.CampaingStatus[worldData.startingLevelIndex] = 1;
        SavePlayerData();
    }

    public Sprite GetSprite (CurrencyType currencyType)
    {
        return spritesCurrencyTypes[(int) currencyType];
    }

    public void SetTutorialDone ()
    {
        playerData.doneTutorial = true;
        SavePlayerData();
        Destroy(instanceTutorial.gameObject);
    }
}

[System.Serializable]
class SpritesPerArmy
{
    public List<Sprite> sprites = new List<Sprite>();
}

public enum CurrencyType
{
    Gold,
    Diamond
}