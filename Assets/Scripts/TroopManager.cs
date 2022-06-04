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

        var prefabUnits = GamePlayManager.Get().CurrentLevelPrefabUnits;
        var prefabProjectiles = GamePlayManager.Get().CurrentLevelPrefabProjectiles;

        if (prefabUnits == null || prefabUnits.Length <= tropIndex)
        {
            Debug.LogWarning("No existe el indice de esa tropa!!");
            return;
        }

        float random = Random.Range(0, 1.0f);
        Vector3 pos = Vector3.Lerp(startPos, endPos, random);

        Unit unit = Instantiate(prefabUnits[tropIndex], pos, Quaternion.identity, transform);
        unit.gameObject.layer = layerToTroop;
        unit.signDirection = unitsGoToRight ? 1 : -1;
        unit.interactableMask = layerToInteract;
        unit.enemyMask = layerToAttack;
        unitsAlive.Add(unit);

        //int indexImage = (int)GameManager.Get().unitsStatsLoaded[tropIndex].tempCurrentShape;                                  // Temporal
        //unitGameObject.GetComponent<MeshFilter>().mesh = GameManager.Get().GetCurrentMesh(indexImage);                         // Temporal
        //unitGameObject.GetComponent<MeshRenderer>().material.color = GameManager.Get().unitsStatsLoaded[tropIndex].tempColor;  // Temporal

        var arrow = unit.gameObject.GetComponentInChildren<UITeamArrow>();
        if (arrow) arrow.SetColor(troopColor);                                                                                   // Temporal

        UnitStats unitStats = GameManager.Get().GetUnitStats(tropIndex);
        unit.SetValues(unitStats, GameManager.Get().GetLevelUnitsPlayer()[tropIndex]);

        var unitShootBehaviour = unit.GetComponent<UnitShootBehaviour>();
        if (unitShootBehaviour) 
        {
            Projectile currentProjectilePrefab = prefabProjectiles[(int)unitStats.attackType];
            unitShootBehaviour.SetPrefabProjectile(currentProjectilePrefab);
        }
    }
}