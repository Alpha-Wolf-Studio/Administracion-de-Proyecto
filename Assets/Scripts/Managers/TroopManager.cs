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
    [SerializeField] private Material mercenaryMaterial;
    //[SerializeField] private Color troopColor = Color.blue; // Temporal

    private ControlPointData currentControlPointData = new ControlPointData();
    private List<KeyValuePair<int, Unit>> mercenaryUnitsSpawned = new List<KeyValuePair<int, Unit>>();
    private List<KeyValuePair<int, Unit>> armyUnitsSpawned = new List<KeyValuePair<int, Unit>>();

    private void Awake()
    {
        currentControlPointData.ResetControlPointBonus();
        
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

        ControlPointWithEnemies.OnControlPointGet += ControlPointUnlocked;
    }

    private void Start()
    {
        lanes[selectedLaneIndex].Selected = true;
    }
    
    private void OnDestroy()
    {
        ControlPointWithEnemies.OnControlPointGet -= ControlPointUnlocked;
        GameManager.Get().SetPostLevelUnits(armyUnitsSpawned, mercenaryUnitsSpawned);
    }


    private void ControlPointUnlocked(ControlPointData controlPointData, Transform[] unlockPoints)
    {
        currentControlPointData.AddControlPointBonus(controlPointData);

        foreach (var unitKeyValuePair in armyUnitsSpawned)
        {
            SetUnitBonuses(unitKeyValuePair.Value);
        }
    }
    
    public void OnButtonCreateTroop(int troopIndex, int unitDataIndex, MilitaryType militaryType)
    {
        Unit[] prefabUnits = GamePlayManager.Get().CurrentLevelPrefabUnits;
        Projectile[] prefabProjectiles = GamePlayManager.Get().CurrentLevelPrefabProjectiles;

        if (prefabUnits == null || prefabUnits.Length <= troopIndex)
        {
            Debug.LogWarning("No existe el indice de esa tropa!!");
            return;
        }


        Vector3 spawnPosition = lanes[selectedLaneIndex].StartPosition;

        Unit unit = Instantiate(prefabUnits[troopIndex], spawnPosition, Quaternion.identity, transform);
        unit.gameObject.layer = layerToTroop;
        unit.signDirection = unitsGoToRight ? 1 : -1;
        unit.interactableMask = layerToInteract;
        unit.enemyMask = layerToAttack;
        unit.AttackLaneFlags = lanes[selectedLaneIndex].LaneFlags;
        unit.OwnLaneFlags = lanes[selectedLaneIndex].LaneFlags;

        //int indexImage = (int)GameManager.Get().unitsStatsLoaded[tropIndex].tempCurrentShape;                                  // Temporal
        //unitGameObject.GetComponent<MeshFilter>().mesh = GameManager.Get().GetCurrentMesh(indexImage);                         // Temporal
        //unitGameObject.GetComponent<MeshRenderer>().material.color = GameManager.Get().unitsStatsLoaded[tropIndex].tempColor;  // Temporal

        //var arrow = unit.gameObject.GetComponentInChildren<UITeamArrow>();                                                     // Temporal
        //if (arrow) arrow.SetColor(troopColor);                                                                                 // Temporal

        UnitStats unitStats = GameManager.Get().GetUnitStats(troopIndex);
        
        switch (militaryType)
        {
            case MilitaryType.Army:
                unit.SetValues(unitStats, GameManager.Get().GetLevelUnitsArmyPlayer()[troopIndex]);
                unit.stats.life = GameManager.Get().GetLifeUnitsArmyPlayer(unitDataIndex);
                armyUnitsSpawned.Add(new KeyValuePair<int, Unit>(unitDataIndex, unit));
                break;
            case MilitaryType.Mercenary:
                unit.SetValues(unitStats, GameManager.Get().GetLevelUnitsMercenaryPlayer()[troopIndex]);
                unit.stats.life = GameManager.Get().GetLifeUnitsMercenaryPlayer(unitDataIndex);
                mercenaryUnitsSpawned.Add(new KeyValuePair<int, Unit>(unitDataIndex, unit));
                unit.SetBaseTroopMaterial(mercenaryMaterial);
                break;
        }

        IShootBehaviour unitShootBehaviour = unit.GetComponent<IShootBehaviour>();
        if (unitShootBehaviour != null) 
        {
            Projectile currentProjectilePrefab = prefabProjectiles[(int)unitStats.attackType];
            unitShootBehaviour.SetPrefabProjectile(currentProjectilePrefab);
        }
        
        SetUnitBonuses(unit);
    }

    private void SetUnitBonuses(Unit unit)
    {
        if (!(unit.stats.life > 0)) return;
        
        unit.stats.damage += unit.stats.damage * currentControlPointData.unlockBonusDamage / 100;
        unit.stats.bonusRange += (unit.stats.rangeAttack * currentControlPointData.unlockBonusRange) / 100;
        unit.Heal(unit.initialStats.life * currentControlPointData.unlockHealAmount / 100);
    }
}