using System;
using UnityEngine;

public class LaneControl : MonoBehaviour
{
    [SerializeField] private Lane[] allLanes = default;

    private void Awake()
    {
        ControlPointWithEnemies.OnControlPointGet += OnControlPointGet;
    }

    private void OnDestroy()
    {
        ControlPointWithEnemies.OnControlPointGet -= OnControlPointGet;
    }

    private void OnControlPointGet(ControlPointData data, Transform[] controlUnlockPoint)
    {
        if (data.controlLanesFlags.HasFlag(LanesFlags.Top))
            allLanes[0].StartPosition = controlUnlockPoint[0].position;
        if(data.controlLanesFlags.HasFlag(LanesFlags.Mid))
            allLanes[1].StartPosition = controlUnlockPoint[1].position;
        if(data.controlLanesFlags.HasFlag(LanesFlags.Bottom))
            allLanes[2].StartPosition = controlUnlockPoint[2].position;
    }
}
