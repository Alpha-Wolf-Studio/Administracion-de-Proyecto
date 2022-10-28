using UnityEngine;

[CreateAssetMenu(fileName = "World Builder Data", menuName = "Editor/World Builder Data")]
public class WorldBuilderData : ScriptableObject
{

    [Header("Map Build Data")]
    public int Rows;
    public int Columns;
    public float XDistance = 2.035f;
    public float ZDistance = -1.17f;
    public int startingLevelIndex = 97;
    public HexagonTerrain PfHexagon;
    public ProvincesData ProvincesData;
    public LevelsData LevelsData;

}
