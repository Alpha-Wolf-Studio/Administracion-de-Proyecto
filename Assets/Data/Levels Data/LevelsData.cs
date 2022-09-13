using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "All Levels Data", menuName = "Editor/All Levels Data")]
public class LevelsData : ScriptableObject
{

    public List<LevelData> levelsList = new List<LevelData>();

    public void ClearTerrainData() => levelsList.Clear();

    public void AddTerrainData(LevelData data) 
    {
        if(levelsList.Exists(i => i.Index == data.Index)) 
        {
            int index = levelsList.FindIndex(i => i.Index == data.Index);
            levelsList[index] = data;
        }
        else 
        {
            levelsList.Add(data);
        }
    }

    public LevelData GetLevelData(int index) 
    {

        if(levelsList.Exists(i => i.Index == index)) 
        {
            var level = levelsList.Find(i => i.Index == index);
            return level;
        }

        return null;

    }

    public void SortListByIndex()
    {
        levelsList.Sort((a, b) => a.Index < b.Index ? 0 : 1);
    }

    public void SortListByName()
    {
        levelsList.Sort((a, b) => string.Compare(a.LevelName, b.LevelName));
    }

}
