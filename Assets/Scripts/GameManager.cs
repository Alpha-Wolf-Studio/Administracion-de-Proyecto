using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public Action OnLoadedStats;
    public static string unitsStatsPath = "UnitStat";
    public List<UnitStats> unitsStatsLoaded = new List<UnitStats>();
    [SerializeField] private Mesh[] meshes;
    [SerializeField] private Sprite[] sprites;
    
    public bool gameover;
    public override void Awake()
    {
        base.Awake();
        LoadAllStatsSaved();
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
        OnLoadedStats?.Invoke();
    }
    public UnitStats GetUnitStats(int index) => unitsStatsLoaded[index];
    public Mesh GetCurrentMesh(int index) => meshes[index];
    public Sprite GetCurrentSprite(int index) => sprites[index];
}