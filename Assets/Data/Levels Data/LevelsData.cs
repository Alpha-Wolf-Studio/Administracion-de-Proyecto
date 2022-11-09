using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "All Levels Data", menuName = "Editor/All Levels Data")]
public class LevelsData : ScriptableObject
{

    public List<LevelData> levelsList = new List<LevelData>();

    public void ClearTerrainData() => levelsList.Clear();


    public void AddEnemiesData(List<EnemyConfigurations> enemies, int index)
    {
#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
#endif
    }
    
    public void AddControlPointsData(List<ControlPointConfigurations> controlPoints, int index)
    {
#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
#endif
    }
    
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
        if (levelsList.Count > index)
        {
            var level = levelsList[index];
            return level;
        }
        return null;
    }

    public LevelData GetLevelDataByFalseIndex(int index)
    {
        if (levelsList.Exists(i => i.Index == index))
        {
            var level = levelsList.Find(i => i.Index == index);
            return level;
        }
        return null;
    }
    
    public void SortListByIndex()
    {
        levelsList.Sort((a, b) => a.Index.CompareTo(b.Index));
    }

    public void SortListByName()
    {
        levelsList.Sort(delegate(LevelData a, LevelData b)
        {
            var aSplit = a.LevelName.Split(' ');
            var bSplit = b.LevelName.Split(' ');

            int aLevelValue = 0;
            if (int.TryParse(aSplit[1], out aLevelValue))
            {
                int bLevelValue = 0;
                if (int.TryParse(bSplit[1], out bLevelValue))
                {
                    return aLevelValue.CompareTo(bLevelValue);
                }
            }

            return 0;
        });
    }

}
