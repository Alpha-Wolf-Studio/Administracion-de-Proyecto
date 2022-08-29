using UnityEngine;

public class HexagonTerrain : MonoBehaviour
{

    public System.Action OnSelect = default;
    public System.Action OnDeSelect = default;

    [SerializeField] private MeshRenderer stateMesh = default;
    [SerializeField] private MeshRenderer provinceMesh = default;
    [SerializeField] int provinceIndex = 0;

    private TerrainManager.TerrainState currentState = default;

    public int TerrainIndex 
    {
        get;
        set;
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
            ChangeTerrainState(currentState);
        }
    }

    public void Init (TerrainManager.TerrainState terrainStatus, int terrainIndex)
    {
        ChangeTerrainState(terrainStatus);
        TerrainIndex = terrainIndex;
        provinceMesh.material.color = Province.GetProvinceColor(provinceIndex);
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

    public void Select () => OnSelect?.Invoke();
    public void Deselect () => OnDeSelect?.Invoke();

}