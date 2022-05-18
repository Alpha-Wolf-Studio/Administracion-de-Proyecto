using System.Collections.Generic;
using UnityEngine;

public class Trench : MonoBehaviour
{

    [SerializeField] private List<Transform> coverageTransforms = new List<Transform>();
    private List<CoveragePosition> coveragePositions = new List<CoveragePosition>();
    private bool hasTroops = false;
    private int currentTroopsLayer = -1;

    public List<CoveragePosition> GetCoveragesPositions() => coveragePositions;
    public bool HasTroops => hasTroops;
    public int CurrentTroopsLayer => currentTroopsLayer;

    public System.Action<int> OnCurrentTroopsLayerChanged;


    private void Awake()
    {

    }

    // Start is called before the first frame update
    private void Start()
    {
        for (int i = 0; i < coverageTransforms.Count; i++)
        {
            CoveragePosition coveragePosition = new CoveragePosition();
            coveragePosition.transform = coverageTransforms[i];
            coveragePositions.Add(coveragePosition);
        }

    }

    private void CheckForTroops() 
    {
        hasTroops = false;
        foreach (var coverageLocation in coveragePositions)
        {
            if (coverageLocation.occupant != null) hasTroops = true;
        }
    }

    public bool IsCoverageFree(int layer) 
    {

        if (hasTroops && layer != currentTroopsLayer) return false;

        foreach (var position in coveragePositions)
        {
            if (!position.occupant) return true; 
        }
        return false;
    }

    public Transform GetFreePosition(Unit unit) 
    {
        if (hasTroops && unit.gameObject.layer != currentTroopsLayer) return null;

        foreach (var coverageLocation in coveragePositions)
        {
            if (!coverageLocation.occupant)
            {
                coverageLocation.occupant = unit;
                currentTroopsLayer = unit.gameObject.layer;
                OnCurrentTroopsLayerChanged?.Invoke(currentTroopsLayer);
                unit.OnDie += coverageLocation.Vacate;
                unit.OnDie += CheckForTroops;
                hasTroops = true;
                return coverageLocation.transform; 
            }
        }
        return null;
    }

    public void ReleaseUnitsFromTrench() 
    {
        foreach (var position in coveragePositions)
        {
            if (position.occupant) 
            {
                position.occupant.GetComponent<UnitHideBehaviour>().GetOut();
            }
        }
    }

    [System.Serializable]
    public class CoveragePosition 
    {
        public Transform transform;

        public Unit occupant = null;
        public void Vacate() => occupant = null;
    }

}
