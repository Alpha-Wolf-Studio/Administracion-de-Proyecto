using System.Collections.Generic;
using UnityEngine;

public class TroopManager : MonoBehaviour
{
    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform endPosition;

    [SerializeField] private int layerToTroop;
    [SerializeField] private LayerMask layerToInteract;
    [SerializeField] private LayerMask layerToAttack;
    [SerializeField] private bool unitsGoToRight = true;
    [SerializeField] private GameObject prefabUnits;
    [SerializeField] private List<Unit> unitsAlive;

    public void OnButtonCreateTroop(int tropIndex)
    {
        Vector3 startPos = startPosition.position;
        Vector3 endPos = endPosition.position; 

        if (!prefabUnits)
        {
            Debug.LogWarning("No existe el indice de esa tropa!!");
            return;
        }

        float random = Random.Range(0, 1.0f);
        Vector3 pos = Vector3.Lerp(startPos, endPos, random);

        GameObject unitGameObject = Instantiate(prefabUnits, pos, Quaternion.identity, transform);
        unitGameObject.gameObject.layer = layerToTroop;
        Unit unit = unitGameObject.GetComponent<Unit>();
        unit.signDirection = unitsGoToRight ? 1 : -1;
        unit.interactableMask = layerToInteract;
        unit.enemyMask = layerToAttack;
        unitsAlive.Add(unit);

        int indexImage = (int)GameManager.Get().unitsStatsLoaded[tropIndex].tempCurrentShape;                                  // Temporal
        unitGameObject.GetComponent<MeshFilter>().mesh = GameManager.Get().GetCurrentMesh(indexImage);                         // Temporal
        unitGameObject.GetComponent<MeshRenderer>().material.color = GameManager.Get().unitsStatsLoaded[tropIndex].tempColor;  // Temporal


        UnitStats unitStats = GameManager.Get().GetUnitStats(tropIndex);
        unit.SetValues(unitStats);
    }
}