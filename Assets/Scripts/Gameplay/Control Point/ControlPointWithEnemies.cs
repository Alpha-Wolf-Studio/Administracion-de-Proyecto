using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ControlPointWithEnemies : MonoBehaviour
{

    public static Action<ControlPointData, Transform[]> OnControlPointGet = default;

    [SerializeField] private ControlPointData controlPointData = default;
    [SerializeField] private List<Enemy> enemiesToControl = default;
    
    [Header("Unlock Points Configurations")]
    [SerializeField] private Transform[] controlUnlockPositions = default;
    [SerializeField] private GameObject graphic = default;
    [SerializeField] private float laneZOffset = 10f;
    [SerializeField] private float size1Lane = 10f;
    [SerializeField] private float size2Lanes = 20f;
    [SerializeField] private float size3Lanes = 30f;
    
    private int unitsToUnlock = 0;
    private int checkPointSize = 0;
    
    private void OnValidate()
    {
        SetCheckpointSize();
        SetCheckpointLocation();
    }

    private void OnDestroy()
    {
        foreach (var enemy in enemiesToControl)
        {
            enemy.Unit.OnDie -= OnControllingUnitDestroyed;
        }
    }

    public void AssignData(ControlPointData controlData, List<Enemy> enemies)
    {
        controlPointData = controlData;
        
        enemiesToControl = enemies;
        unitsToUnlock = enemies.Count;
        
        foreach (var enemy in enemiesToControl)
        {
            enemy.Unit.OnDie += OnControllingUnitDestroyed;
        }
        
        SetCheckpointSize();
        SetCheckpointLocation();
    }

    private void OnControllingUnitDestroyed()
    {
        unitsToUnlock--;
        if (unitsToUnlock == 0)
        {
            OnControlPointGet?.Invoke(controlPointData, controlUnlockPositions);
            Destroy(gameObject);
        }
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
            
        var controlEnemyIndexes = enemiesToControl.Select(enemy => enemy.EnemyIndex).ToList();
        configuration.ControlEnemiesIndex = controlEnemyIndexes;
        return configuration;
    }


    private void SetCheckpointSize()
    {
        checkPointSize = 0;
        if(LaneFlagSelected(LanesFlags.Bottom)) checkPointSize++;
        if(LaneFlagSelected(LanesFlags.Mid)) checkPointSize++;
        if(LaneFlagSelected(LanesFlags.Top)) checkPointSize++;

        Vector3 newScale = graphic.transform.localScale;
        
        switch (checkPointSize)
        {
            case 1:
                newScale.z = size1Lane;
                break;
            
            case 2:
                if (LaneFlagSelected(LanesFlags.Bottom) && LaneFlagSelected(LanesFlags.Top))
                    newScale.z = size3Lanes;
                else
                    newScale.z = size2Lanes;
                break;
            
            case 3:
                newScale.z = size3Lanes;
                break;
            
            default:
                newScale.z = size1Lane;
                break;
            
        }

        graphic.transform.localScale = newScale;
    }

    private void SetCheckpointLocation()
    {

        Vector3 newPosition = graphic.transform.localPosition;
        newPosition.x = 0;
        newPosition.z = 0;
        
        switch (checkPointSize)
        {
            case 1:
                if(LaneFlagSelected(LanesFlags.Top)) newPosition.z += laneZOffset;
                if(LaneFlagSelected(LanesFlags.Bottom)) newPosition.z -= laneZOffset;
                break;
            
            case 2:
                if (LaneFlagSelected(LanesFlags.Mid))
                {
                    if(LaneFlagSelected(LanesFlags.Top)) newPosition.z += laneZOffset / 2;
                    if(LaneFlagSelected(LanesFlags.Bottom)) newPosition.z -= laneZOffset / 2;
                }
                break;

        }
        graphic.transform.localPosition = newPosition;

    }

    private bool LaneFlagSelected(LanesFlags flag)
    {
        return controlPointData.controlLanesFlags.HasFlag(flag);
    }
}
