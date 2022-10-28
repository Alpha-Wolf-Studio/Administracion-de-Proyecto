using UnityEngine;

[System.Serializable]
public class ControlPointData
{
    public LanesFlags controlLanesFlags = default;
    [Range(0, 100)] public float percentageBonusDamage = 0;
    [Range(0, 100)] public float percentageBonusAttackSpeed = 0;
    [Range(0, 100)] public float percentageBonusHp = 0;
}
