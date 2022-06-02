using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public Action onLoadedStats;
    public static string unitsStatsPath = "UnitStat";
    public List<UnitStats> unitsStatsLoaded = new List<UnitStats>();
    [SerializeField] private Mesh[] meshes;
    [SerializeField] private Sprite[] sprites;

    private string pathPlayerData = "PlayerData";
    private PlayerData playerData;

    public bool gameover;

    public override void Awake()
    {
        base.Awake();
        LoadAllStatsSaved();
        playerData = JsonUtility.FromJson<PlayerData>(LoadAndSave.LoadFromFile(pathPlayerData));
    }

    private void Start()
    {
        Time.timeScale = 1;

        //for (int i = 0; i < 5; i++)
        //{
        //    UnitStats unit = new UnitStats();
        //    string data = JsonUtility.ToJson(unit,true);
        //    LoadAndSave.SaveToFile(unitsStatsPath + i, data);
        //}
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

    public void AddLevelPlayer()
    {
        playerData.currentLevel++;
        SavePlayerData();
    }

    /// <summary>
    /// Dinero que se suma o se resta.
    /// </summary>
    /// <param name="plusMoney"></param>
    /// <returns>Si se pudo restar el dinero. Lo resta y guarda en caso de que si</returns>
    public bool ModifyMoneyPlayer(int plusMoney)
    {
        if (playerData.currentMoney + plusMoney < 0)
            return false;
        playerData.currentMoney += plusMoney;
        SavePlayerData();
        return true;
    }

    public void SetPlayerDataName(string newName)
    {
        playerData.playerName = newName;
        SavePlayerData();
    }

    public string GetPlayerName() => playerData.playerName;
    public int[] GetLevelUnitsPlayer() => playerData.levelUnits;
    public int GetLevelPlayer() => playerData.currentLevel;
    public void AddLevelUnit(int i)
    {
        playerData.levelUnits[i]++;
        SavePlayerData();
    } 
    private void SavePlayerData() => LoadAndSave.SaveToFile(pathPlayerData, JsonUtility.ToJson(playerData, true));
    public UnitStats GetUnitStats(int index) => unitsStatsLoaded[index];
    public int GetLevelsUnits(int i) => playerData.levelUnits[i];
    public float GetMoneyPlayer() => playerData.currentMoney;
    public Mesh GetCurrentMesh(int index) => meshes[index];
    public Sprite GetCurrentSprite(int index) => sprites[index];
}