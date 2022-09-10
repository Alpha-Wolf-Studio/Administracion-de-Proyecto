using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerCampaignManager : MonoBehaviour
{
    [Header("Terrain")]
    [SerializeField] private TerrainManager terrainManager = default;
    [SerializeField] private LayerMask terrainLayer = default;
    [SerializeField] private float cursorMaxDistance = 100f;

    [Header("Camera")]
    [SerializeField] private float campaingSelectionStartTime = .5f;
    [SerializeField] private PlayerCampaignCameraController cameraController = default;
    private Camera mainCamera = default;
    private int lastLevelCompleted = 0;
    private bool selectionStarted = false;

    TerrainEventsHandler currentSelectedTerrain = null;

    private void Awake ()
    {
        mainCamera = Camera.main;
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(campaingSelectionStartTime);
        StartCamera();
    }

    private void StartCamera() 
    {
        lastLevelCompleted = GameManager.Get().GetLastLevelCompleted();
        var hexagon = terrainManager.GetHexagonByIndex(lastLevelCompleted);
        hexagon.Select();
        cameraController.SetTarget(hexagon.transform);
        selectionStarted = true;
    }

    private void Update ()
    {

        if (EventSystem.current.IsPointerOverGameObject() || !selectionStarted) return;

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
                        cameraController.SetTarget(currentSelectedTerrain.transform);
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
