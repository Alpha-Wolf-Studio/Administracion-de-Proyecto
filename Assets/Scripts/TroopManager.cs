using System.Collections.Generic;
using UnityEngine;

public class TroopManager : MonoBehaviour
{
    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform endPosition;

    [SerializeField] private int layerToTroop;
    [SerializeField] private LayerMask layerToInteract;
    [SerializeField] private bool unitsGoToRight = true;
    [SerializeField] private GameObject[] prefabUnits;
    [SerializeField] private UnitsData unitStats;
    [SerializeField] private List<Unit> unitsAlive;

    public void OnButtonCreateTroop(int tropIndex)
    {
        Vector3 startPos = startPosition.position;
        Vector3 endPos = endPosition.position; 

        if (!prefabUnits[tropIndex])
        {
            Debug.LogWarning("No existe el indce de esa tropa!!");
            return;
        }

        float random = Random.Range(0, 1.0f);
        Vector3 pos = Vector3.Lerp(startPos, endPos, random);

        GameObject unitGameObject = Instantiate(prefabUnits[tropIndex], pos, Quaternion.identity, transform);
        unitGameObject.gameObject.layer = layerToTroop;
        Unit unit = unitGameObject.GetComponent<Unit>();
        unit.signDirection = unitsGoToRight ? 1 : -1;
        unit.layerMaskInteraction = layerToInteract;
        unitsAlive.Add(unit);

        UnitStats unitStats = new UnitStats(this.unitStats.unitsStats[tropIndex]);
        unit.SetValues(unitStats);
    }
}