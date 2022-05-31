using System.Collections.Generic;
using UnityEngine;

public class TroopManager : MonoBehaviour
{
    [SerializeField] private Unit baseSpawnUnit = default;
    [SerializeField] private Transform startPosition = default;
    [SerializeField] private Transform endPosition = default;

    [SerializeField] private int layerToTroop = 0;
    [SerializeField] private LayerMask layerToInteract = default;
    [SerializeField] private LayerMask layerToAttack = default;
    [SerializeField] private bool unitsGoToRight = true;
    [SerializeField] private GameObject prefabUnits = default;
    [SerializeField] private List<Unit> unitsAlive = default;
    [SerializeField] private Color troopColor = Color.blue; // Temporal

    private void Start()
    {
        UnitStats unitStat = new UnitStats(); //TODO CAMBIAR STATS DE TRINCHERAS POR LEIDAS DE ALGUN LADO
        unitStat.life = 100f;
        baseSpawnUnit.SetValues(unitStat, 0);
    }

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

        //int indexImage = (int)GameManager.Get().unitsStatsLoaded[tropIndex].tempCurrentShape;                                  // Temporal
        //unitGameObject.GetComponent<MeshFilter>().mesh = GameManager.Get().GetCurrentMesh(indexImage);                         // Temporal
        //unitGameObject.GetComponent<MeshRenderer>().material.color = GameManager.Get().unitsStatsLoaded[tropIndex].tempColor;  // Temporal

        var arrow = unitGameObject.GetComponentInChildren<UITeamArrow>();
        if (arrow) arrow.SetColor(troopColor);                                                                                   // Temporal


        UnitStats unitStats = GameManager.Get().GetUnitStats(tropIndex);
        unit.SetValues(unitStats, GameManager.Get().GetLevelPlayer()[tropIndex]);
    }
}