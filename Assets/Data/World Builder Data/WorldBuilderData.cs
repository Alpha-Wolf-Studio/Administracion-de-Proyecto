using UnityEngine;

[CreateAssetMenu(fileName = "World Builder Data", menuName = "Editor/World Builder Data")]
public class WorldBuilderData : ScriptableObject
{

    [Header("Map Build Data")]
    public int Rows;
    public int Columns;
    public float xDistance = 2.035f;
    public float zDistance = -1.17f;
    public HexagonTerrain pfHexagon;


}
