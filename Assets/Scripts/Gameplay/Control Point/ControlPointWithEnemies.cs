using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ControlPointWithEnemies : MonoBehaviour
{

    public static Action<ControlPointData, Transform[]> OnControlPointGet = default;

    [SerializeField] private ControlPointData controlPointData = default;
    [Space(20)]
    [SerializeField] private List<Enemy> enemiesInControlPoint = default;

    [Header("Control Point Stats")] 
    [SerializeField] private float controlPointLife = 25f;
    
    [Header("Unlock Configuration")]
    [SerializeField] private Transform[] unlockPointsTransforms;
    [SerializeField] private int flagMaterialIndex = 1;
    [SerializeField] private List<Renderer> flagsRenderers;

    [Header("3 Lanes Control Point")]
    [SerializeField] private GameObject baseControlPoint3Lanes;

    
    [Header("2 Lanes Control Point")]
    [SerializeField] private GameObject baseControlPoint2Lanes;
    [SerializeField] private GameObject controlPoint2LanesTop;
    [SerializeField] private GameObject controlPoint2LanesBot;
    
    [Header("1 Lane Control Point")]
    [SerializeField] private GameObject baseControlPoint1Lane;
    [SerializeField] private GameObject controlPoint1LaneTop;
    [SerializeField] private GameObject controlPoint1LaneMid;
    [SerializeField] private GameObject controlPoint1LaneBot;
    
    public Unit Unit => unit;
    public ControlPointData ControlPointData => controlPointData;
    
    private int unitsToUnlock = 0;
    private int checkPointSize = 0;
    private Unit unit = null;

    private bool isUnlocked = false;

#if UNITY_EDITOR
    private void OnValidate()
    {
        UnityEditor.EditorApplication.delayCall += CustomValidate;
    }
#endif

    private void CustomValidate()
    {
        if (this == null) 
            return;
        
        SetCheckpointSize();
    }

    public void AssignData(ControlPointData controlData, List<Enemy> enemies)
    {
        controlPointData = controlData;
        
        enemiesInControlPoint = enemies;
        unitsToUnlock = enemies.Count;

        SetCheckpointSize();

        unit = GetComponent<Unit>();
        UnitStats stats = new UnitStats
        {
            life = controlPointLife * (checkPointSize + 1),
            rangeAttack = 0,
            radiusSight = 0,
            unitType = UnitsType.Edification
        };
        unit.OwnLaneFlags = controlPointData.controlLanesFlags;
        unit.SetValues(stats, 0);
        

        foreach (var enemy in enemiesInControlPoint)
        {
            enemy.SetControlPoint(this);
        }

        unit.OnDie += delegate
        {
            if (isUnlocked) return;
            isUnlocked = true;
            
            OnControlPointGet?.Invoke(controlPointData, unlockPointsTransforms);

            foreach (var enemy in enemiesInControlPoint)
            {
                enemy.UnlockRedirectDamageUnit();
                enemy.Unit.TakeDamage(float.MaxValue, enemy.Unit.stats);
            }
        };
    }

    public ControlPointConfigurations GetCurrentConfiguration()
    {
        var configuration = new ControlPointConfigurations
        {
            ControlData = controlPointData
        };
        var ownTransform = transform;
        configuration.ControlPosition = ownTransform.position;
        configuration.ControlRotation = ownTransform.eulerAngles;
            
        var controlEnemyIndexes = enemiesInControlPoint.Select(enemy => enemy.EnemyIndex).ToList();
        configuration.ControlEnemiesIndex = controlEnemyIndexes;
        return configuration;
    }

    private void SetCheckpointSize()
    {
        checkPointSize = 0;
        if(LaneFlagSelected(LanesFlags.Bottom)) checkPointSize++;
        if(LaneFlagSelected(LanesFlags.Mid)) checkPointSize++;
        if(LaneFlagSelected(LanesFlags.Top)) checkPointSize++;

        switch (checkPointSize)
        {
            case 3:
                baseControlPoint1Lane.SetActive(false);
                baseControlPoint2Lanes.SetActive(false);
                baseControlPoint3Lanes.SetActive(true);
                break;
            
            case 2:
                bool isBotSelected = LaneFlagSelected(LanesFlags.Bottom);
                bool isTopSelected = LaneFlagSelected(LanesFlags.Top);

                if (isBotSelected && isTopSelected)
                {
                    baseControlPoint1Lane.SetActive(true);
                    baseControlPoint2Lanes.SetActive(false);
                    baseControlPoint3Lanes.SetActive(false);
                    
                    controlPoint1LaneTop.SetActive(true);
                    controlPoint1LaneMid.SetActive(false);
                    controlPoint1LaneBot.SetActive(true);
                }
                else
                {
                    
                    baseControlPoint1Lane.SetActive(false);
                    baseControlPoint2Lanes.SetActive(true);
                    baseControlPoint3Lanes.SetActive(false);
                    
                    if (isBotSelected)
                    {
                        controlPoint2LanesBot.SetActive(true);
                        controlPoint2LanesTop.SetActive(false);
                    }
                    else
                    {
                        controlPoint2LanesBot.SetActive(false);
                        controlPoint2LanesTop.SetActive(true);
                    }
                }
                
                break;
            
            case 1:
                baseControlPoint1Lane.SetActive(true);
                baseControlPoint2Lanes.SetActive(false);
                baseControlPoint3Lanes.SetActive(false);
            
                controlPoint1LaneTop.SetActive(false);
                controlPoint1LaneMid.SetActive(false);
                controlPoint1LaneBot.SetActive(false);

                if (LaneFlagSelected(LanesFlags.Top)) controlPoint1LaneTop.SetActive(true);
                if(LaneFlagSelected(LanesFlags.Mid)) controlPoint1LaneMid.SetActive(true);
                if(LaneFlagSelected(LanesFlags.Bottom)) controlPoint1LaneBot.SetActive(true);
                break;
            
            default:
                baseControlPoint1Lane.SetActive(false);
                baseControlPoint2Lanes.SetActive(false);
                baseControlPoint3Lanes.SetActive(false);
                break;
        }
        
    }
    

    private bool LaneFlagSelected(LanesFlags flag)
    {
        return controlPointData.controlLanesFlags.HasFlag(flag);
    }
}
