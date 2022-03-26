using System.Collections.Generic;
using UnityEngine;

public class TroopManager : MonoBehaviour
{
    [SerializeField] private int layerToTroop;
    [SerializeField] private LayerMask layerToAttack;
    [SerializeField] private bool unitsGoToRight = true;
    [SerializeField] private GameObject[] prefabUnits;
    [SerializeField] private UnitsData unitStats;
    [SerializeField] private List<Unit> unitsAlive;
    public void OnButtonCreateTroop(int tropIndex)
    {
        if (!prefabUnits[tropIndex])
        {
            Debug.LogWarning("No existe el indce de esa tropa!!");
            return;
        }

        Vector3 pos = transform.position; // Todo: Hacer que sea random dentro del collider.

        GameObject unitGameObject = Instantiate(prefabUnits[tropIndex], pos, Quaternion.identity, transform);
        unitGameObject.gameObject.layer = layerToTroop;
        Unit unit = unitGameObject.GetComponent<Unit>();
        unit.signDirection = unitsGoToRight ? 1 : -1;
        unit.layerMaskEnemy = layerToAttack;
        unitsAlive.Add(unit);

        UnitStats unitStats = new UnitStats(this.unitStats.unitsStats[tropIndex]);
        unit.SetValues(unitStats);
    }
}