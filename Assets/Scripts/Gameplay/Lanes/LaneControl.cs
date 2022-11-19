using UnityEngine;

public class LaneControl : MonoBehaviour
{
    [SerializeField] private Lane[] allLanes = default;

    private void Awake()
    {
        ControlPointWithEnemies.OnControlPointGet += delegate(ControlPointData data, Transform[] controlUnLockPoints)
        {
            if (data.controlLanesFlags.HasFlag(LanesFlags.Top))
                allLanes[0].StartPosition = controlUnLockPoints[0].position;
            if(data.controlLanesFlags.HasFlag(LanesFlags.Mid))
                allLanes[1].StartPosition = controlUnLockPoints[1].position;
            if(data.controlLanesFlags.HasFlag(LanesFlags.Bottom))
                allLanes[2].StartPosition = controlUnLockPoints[2].position;
        };
    }

}
