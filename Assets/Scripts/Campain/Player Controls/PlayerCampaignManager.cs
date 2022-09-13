using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerCampaignManager : MonoBehaviour
{

    public System.Action<HexagonTerrain> OnSelectionChange;

    [Header("Terrain")]
    [SerializeField] private TerrainManager terrainManager = default;
    [SerializeField] private LayerMask terrainLayer = default;
    [SerializeField] private float cursorMaxDistance = 100f;

    [Header("Camera")]
    [SerializeField] private float campaignSelectionStartTime = .5f;
    [SerializeField] private PlayerCampaignCameraController cameraController = default;
    private Camera mainCamera = default;
    private int lastLevelCompleted = 0;
    private bool selectionStarted = false;

    TerrainEventsHandler currentSelectedTerrain = null;
    public HexagonTerrain GetCurrentSelectedTerrain()
    {
        if (currentSelectedTerrain != null) return currentSelectedTerrain.Terrain;
        else return null;
    }

    private void Awake ()
    {
        mainCamera = Camera.main;
        terrainManager.OnResetTerrainStates += OnResetTerrain;
        terrainManager.OnSaveTerrainStates += OnSaveTerrain;
    }

    private IEnumerator Start()
    {
        lastLevelCompleted = GameManager.Get().GetLastLevelCompleted();
        var hexagon = terrainManager.GetHexagonByIndex(lastLevelCompleted);
        var hexagonEventHandler = hexagon.GetComponentInChildren<TerrainEventsHandler>();
        currentSelectedTerrain = hexagonEventHandler;
        OnSelectionChange?.Invoke(currentSelectedTerrain.Terrain);
        yield return new WaitForSeconds(campaignSelectionStartTime);
        StartCamera();
    }
    private void Update ()
    {

        if (EventSystem.current.IsPointerOverGameObject() || !selectionStarted) return;

        if (Input.GetMouseButtonDown(0)) 
        {

            Vector3 mousePosition = Input.mousePosition;
            Ray ray = mainCamera.ScreenPointToRay(mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, cursorMaxDistance, terrainLayer))
            {
                var terrainEventHandler = hit.collider.GetComponent<TerrainEventsHandler>();
                if (terrainEventHandler)
                {
                    if (terrainEventHandler.Terrain.CurrentState != TerrainManager.TerrainState.Unavailable)
                    {
                        currentSelectedTerrain?.Deselect();
                        currentSelectedTerrain = terrainEventHandler;
                        currentSelectedTerrain?.Select();
                        cameraController.SetTarget(currentSelectedTerrain.transform);
                        GameManager.Get().CurrentSelectedLevel = currentSelectedTerrain.Terrain.GetLevelData();
                        OnSelectionChange?.Invoke(currentSelectedTerrain.Terrain);

                    }
                }
            }

        }
    }

    private void OnDestroy()
    {
        terrainManager.OnResetTerrainStates -= OnResetTerrain;
    }

    private void OnResetTerrain(HexagonTerrain terrain) 
    {
        terrain.Select();
        cameraController.SetTarget(terrain.transform);
        currentSelectedTerrain = null;
    }

    private void OnSaveTerrain()
    {
        GameManager.Get().SetLastLevelPlayer(0);
    }

    private void StartCamera()
    {
        currentSelectedTerrain.Select();
        cameraController.SetTarget(currentSelectedTerrain.transform);
        selectionStarted = true;
    }

    public void CompleteCurrentLevel() 
    {
        if (!SelectedStateIsWinnable()) return;
        GameManager.Get().OnLevelWin(currentSelectedTerrain.Terrain.TerrainIndex);
        terrainManager.GetCurrentHexagonStates();
    }

    private bool SelectedStateIsWinnable() => currentSelectedTerrain != null && currentSelectedTerrain.Terrain.CurrentState != TerrainManager.TerrainState.Unlocked;

}
