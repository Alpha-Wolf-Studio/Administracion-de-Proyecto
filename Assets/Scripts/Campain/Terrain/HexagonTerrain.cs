using UnityEngine;

public class HexagonTerrain : MonoBehaviour
{

    public System.Action OnSelect = default;
    public System.Action OnDeSelect = default;

    [SerializeField] private MeshRenderer stateMesh = default;
    [SerializeField] private MeshRenderer provinceMesh = default;
    
    public int index = 0;
    
    private TerrainManager.TerrainState currentState = default;

    private LevelData levelData = new LevelData();
    public LevelData GetLevelData() => levelData;
    
    public bool IsValid { get; set; }

    public void ResetHexagonData() 
    {
        int previousIndex = levelData.Index;
        levelData = new LevelData();
        levelData.Index = previousIndex;
        SetData(levelData);
    }

    public int TerrainIndex 
    {
        get 
        {
            return levelData.Index;
        }
        private set 
        {
            levelData.Index = value;
        }
    }

    public int ProvinceIndex
    {
        get 
        {
            return levelData.ProvinceIndex;
        }
        private set
        {
            levelData.ProvinceIndex = value;
            ChangeProvinceIndex(value);
        }
    }


    public TerrainManager.TerrainState CurrentState
    {
        get 
        {
            return currentState;
        }
        set
        {
            currentState = value;
            ChangeTerrainState(value);
        }
    }

    public string TerrainName => levelData.LevelName;
    public int GoldIncome => levelData.GoldIncome;
    public int GoldOnComplete => levelData.GoldOnComplete;

    public void InitStatus (TerrainManager.TerrainState terrainStatus)
    {
        ChangeTerrainState(terrainStatus);
    }

    public void SetData(LevelData data) 
    {
        TerrainIndex = data.Index;
        levelData.LevelName = data.LevelName;
        ProvinceIndex = data.ProvinceIndex;
        levelData.GoldIncome = data.GoldIncome;
        levelData.GoldOnComplete = data.GoldOnComplete;
        levelData.Enemies = data.Enemies;
        IsValid = true;
    }

    private void ChangeTerrainState (TerrainManager.TerrainState terrainState) 
    {
        currentState = terrainState;

        switch (currentState)
        {
            case TerrainManager.TerrainState.Unlocked:
                stateMesh.material.color = Color.green;
                break;
            case TerrainManager.TerrainState.Locked:
                stateMesh.material.color = Color.red;
                break;
            case TerrainManager.TerrainState.Unavailable:
                stateMesh.material.color = Color.black;
                break;
            default:
                break;
        }
    }

    private void ChangeProvinceIndex(int index) 
    {
        var color = GameManager.Get().GetProvinceSetting(index).Color;
        provinceMesh.material.color = color;
    }

    public void Select () => OnSelect?.Invoke();
    public void Deselect () => OnDeSelect?.Invoke();

}