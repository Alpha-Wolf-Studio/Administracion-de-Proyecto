using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trench : MonoBehaviour
{
    private List<Transform> coverageTransforms = new List<Transform>();

    private List<CoveragePosition> coveragePositions = new List<CoveragePosition>();
    private bool hasTroops = false;
    private int currentTroopsLayer = -1;

    public List<CoveragePosition> GetCoveragesPositions() => coveragePositions;

    public bool HasTroops => hasTroops;

    public int CurrentTroopsLayer => currentTroopsLayer;

    [System.Serializable]
    public class CoveragePosition 
    {
        public Transform transform;

        public Unit occupant = null;
        public void Vacate() => occupant = null;
    }

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            coverageTransforms.Add(child);
        }
    }

    // Start is called before the first frame update
    void Start()
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

    public bool IsCoverageFree() 
    {
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
                unit.OnDie += coverageLocation.Vacate;
                unit.OnDie += CheckForTroops;
                hasTroops = true;
                return coverageLocation.transform; 
            }
        }
        return null;
    }

}
