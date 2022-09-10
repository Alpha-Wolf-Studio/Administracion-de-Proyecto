using System;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private Sprite[] sprites;
    [Space(10)]
    [SerializeField] private WorldBuilderData worldData = default;

    private string pathPlayerData = "PlayerData";
    [SerializeField] private PlayerData playerData;

    public override void Awake ()
    {
        base.Awake();
        LoadAllStatsSaved();
        LoadAllPlayerData();
    }

    private void Start ()
    {
        Application.quitting += SavePlayerData;
        Time.timeScale = 1;
    }

    private void LoadAllPlayerData() 
    {
        playerData = JsonUtility.FromJson<PlayerData>(LoadAndSave.LoadFromFile(pathPlayerData));
        if(playerData.CampaingStatus == null) 
        {
            playerData.CampaingStatus = TerrainManager.GetDefaultTerrainEnumIndexes(worldData.Rows, worldData.Columns);
        }
    }

    void LoadAllStatsSaved()
    {
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

    public void CompleteLevelPlayer (int level)
    {
        if(playerData.LastLevelComplete < level) 
        {
            playerData.LastLevelComplete = level;
            SavePlayerData();
        }
    }

    /// <summary>
    /// Dinero que se suma o se resta.
    /// </summary>
    /// <param name="plusMoney"></param>
    /// <returns>Si se pudo restar el dinero. Lo resta y guarda en caso de que si</returns>
    public bool ModifyMoneyPlayer (int plusMoney)
    {
        if (playerData.CurrentMoney + plusMoney < 0)
            return false;
        playerData.CurrentMoney += plusMoney;
        SavePlayerData();
        return true;
    }

    public void SetPlayerDataName (string newName)
    {
        playerData.PlayerName = newName;
        SavePlayerData();
    }

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

    public void OnLevelWin (int level)
    {

        playerData.LastLevelComplete = level;

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

        SavePlayerData();
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
    public int[] GetLevelUnitsPlayer() => playerData.LevelUnits;
    public int GetLevelPlayer() => playerData.LastLevelComplete;
    public void AddLevelUnit(int i)
    {
        playerData.LevelUnits[i]++;
        SavePlayerData();
    }

    private void SavePlayerData ()
    {
        playerData.LastSavedTime = System.DateTime.Now.DayOfYear; // Todo: Actualmente solo guarda el d�a del a�o, hay que tomar el dato de alg�n lado m�s fiable que el local del jugador.
        LoadAndSave.SaveToFile(pathPlayerData, JsonUtility.ToJson(playerData, true));
    }

    public UnitStats GetUnitStats(int index) => unitsStatsLoaded[index];
    public int GetLevelsUnits(int i) => playerData.LevelUnits[i];
    public float GetMoneyPlayer() => playerData.CurrentMoney;
    public Mesh GetCurrentMesh(int index) => meshes[index];
    public Sprite GetCurrentSprite(int index) => sprites[index];
    public int GetLastLevelCompleted() => playerData.LastLevelComplete;

    public void HealAllUnits ()
    {
        foreach (UnitData army in playerData.DataArmies)
        {
            army.Life = unitsStatsLoaded[army.IdUnit].life;
        }
        foreach (UnitData mercenaries in playerData.DataMercenaries)
        {
            mercenaries.Life = unitsStatsLoaded[mercenaries.IdUnit].life;
        }

        SavePlayerData();
        OnHealtAllUnits?.Invoke();
    }

    public void BuyArmy (int idUnit)
    {
        UnitData[] newUnitData = new UnitData[playerData.DataArmies.Length + 1];

        for (int i = 0; i < playerData.DataArmies.Length; i++)
        {
            newUnitData[i] = playerData.DataArmies[i];
        }
        newUnitData[newUnitData.Length - 1] = new UnitData(idUnit, unitsStatsLoaded[idUnit].unitType, unitsStatsLoaded[idUnit].life);

        playerData.DataArmies = newUnitData;
        SavePlayerData();
        OnAddUnitArmy?.Invoke();
    }

    public void BuyMercenary (int idUnit)
    {
        UnitData[] newUnitData = new UnitData[playerData.DataMercenaries.Length + 1];

        for (int i = 0; i < playerData.DataMercenaries.Length; i++)
        {
            newUnitData[i] = playerData.DataMercenaries[i];
        }

        newUnitData[newUnitData.Length - 1] = new UnitData(idUnit, unitsStatsLoaded[idUnit].unitType, unitsStatsLoaded[idUnit].life);

        playerData.DataMercenaries = newUnitData;
        SavePlayerData();
        OnAddUnitMercenary?.Invoke();
    }

    public void LevelUpUnit (int idUnit)
    {
        playerData.LevelUnits[idUnit]++;
        SavePlayerData();
        OnAddLevelUnit?.Invoke();
    }

    private void OnDestroy () // Todo: Probar que esto funcione cuando android te cierra el proceso
    {
        SavePlayerData();
    }

    public UnitData[] GetUnitsArmy () => playerData.DataArmies;
    public UnitData[] GetUnitsMercenary() => playerData.DataMercenaries;
}