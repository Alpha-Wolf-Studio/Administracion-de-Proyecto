using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerCampaignManager : MonoBehaviour
{

    [SerializeField] private TerrainManager terrainManager = default;
    [SerializeField] private LayerMask terrainLayer = default;
    [SerializeField] private float cursorMaxDistance = 100f;

    private Camera mainCamera = default;

    TerrainEventsHandler currentSelectedTerrain = null;

    private void Awake ()
    {
        mainCamera = Camera.main;
    }

    private void Update ()
    {

        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (Input.GetMouseButtonDown(0)) 
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = mainCamera.ScreenPointToRay(mousePosition);

            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, cursorMaxDistance, terrainLayer)) 
            {
                var terrainEventHandler = hit.collider.GetComponent<TerrainEventsHandler>();
                if (terrainEventHandler) 
                {
                    if(terrainEventHandler.Terrain.CurrentState != TerrainManager.TerrainState.Unavailable) 
                    {
                        currentSelectedTerrain?.Deselect();
                        currentSelectedTerrain = terrainEventHandler;
                        currentSelectedTerrain?.Select();
                    }
                }
            }

        }
    }

    public void CompleteCurrentLevel() 
    {
        if (!SelectedStateIsWinnable()) return;
        GameManager.Get().OnLevelWin(currentSelectedTerrain.Terrain.TerrainIndex);
        terrainManager.GetCurrentHexagonStates();
    }

    private bool SelectedStateIsWinnable() => currentSelectedTerrain != null && currentSelectedTerrain.Terrain.CurrentState != TerrainManager.TerrainState.Unlocked;

}
