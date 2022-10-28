using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TerrainEventsHandler : MonoBehaviour
{

    [SerializeField] HexagonTerrain terrain = default;
    public HexagonTerrain Terrain => terrain;

    public void Select()
    {
        terrain.Select();
    }

    public void Deselect()
    { 
        terrain.Deselect();
    }

}
