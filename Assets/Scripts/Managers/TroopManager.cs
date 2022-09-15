using System.Collections.Generic;
using UnityEngine;

public class TroopManager : MonoBehaviour
{

    [SerializeField] private Lane[] lanes = default;
    private int selectedLaneIndex = 0;

    [SerializeField] private int layerToTroop = 0;
    [SerializeField] private LayerMask layerToInteract = default;
    [SerializeField] private LayerMask layerToAttack = default;
    [SerializeField] private bool unitsGoToRight = true;
    [SerializeField] private List<Unit> unitsAlive = default;
    [SerializeField] private Material baseUnitMaterial = default;
    //[SerializeField] private Color troopColor = Color.blue; // Temporal

    private void Awake()
    {
        for (int i = 0; i < lanes.Length; i++)
        {
            lanes[i].SetLaneIndex(i);
            lanes[i].OnLaneSelected += (index) =>
            {
                selectedLaneIndex = index;

                for (int j = 0; j < lanes.Length; j++)
                {
                    lanes[j].Selected = j == selectedLaneIndex;
                }

            };
        }
    }

    private void Start()
    {
        lanes[selectedLaneIndex].Selected = true;
    }

    public void OnButtonCreateTroop(int tropIndex)
    {


        var prefabUnits = GamePlayManager.Get().CurrentLevelPrefabUnits;
        var prefabProjectiles = GamePlayManager.Get().CurrentLevelPrefabProjectiles;

        if (prefabUnits == null || prefabUnits.Length <= tropIndex)
        {
            Debug.LogWarning("No existe el indice de esa tropa!!");
            return;
        }


        Vector3 spawnPosition = lanes[selectedLaneIndex].StartTransform.position;

        Unit unit = Instantiate(prefabUnits[tropIndex], spawnPosition, Quaternion.identity, transform);
        unit.gameObject.layer = layerToTroop;
        unit.signDirection = unitsGoToRight ? 1 : -1;
        unit.interactableMask = layerToInteract;
        unit.enemyMask = layerToAttack;
        unit.AttackLaneFlags = lanes[selectedLaneIndex].LaneFlags;
        unit.OwnLaneFlags = lanes[selectedLaneIndex].LaneFlags;
        unitsAlive.Add(unit);

        //int indexImage = (int)GameManager.Get().unitsStatsLoaded[tropIndex].tempCurrentShape;                                  // Temporal
        //unitGameObject.GetComponent<MeshFilter>().mesh = GameManager.Get().GetCurrentMesh(indexImage);                         // Temporal
        //unitGameObject.GetComponent<MeshRenderer>().material.color = GameManager.Get().unitsStatsLoaded[tropIndex].tempColor;  // Temporal

        //var arrow = unit.gameObject.GetComponentInChildren<UITeamArrow>();                                                     // Temporal
        //if (arrow) arrow.SetColor(troopColor);                                                                                 // Temporal

        unit.SetBaseTroopMaterial(baseUnitMaterial);

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