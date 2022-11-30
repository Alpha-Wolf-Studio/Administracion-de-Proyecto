using System.Collections;
using System.Collections.Generic;
using PrivateClassUpgrades;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrades Progression", menuName = "Upgrades/Upgrades Progression", order = 2)]
public class UpgradeProgression : ScriptableObject
{
    public Expand expand;
    public Heal heal;
    public Buy buy;
    public LvlUp lvlUp;
}

namespace PrivateClassUpgrades
{
    [System.Serializable]
    public class Expand
    {
        public int armyMultiplyPerLevel;
        public int armyMultiplyPerLevelPlus;
        [Space(5)] public int mercenaryMultiplyPerLevel;
        public int mercenaryMultiplyPerLevelPlus;
    }

    [System.Serializable]
    public class Heal
    {
        public int armyMultiplyPerLevel;
        public int armyMultiplyPerLife;
        [Space(5)] public int mercenaryMultiplyPerLevel;
        public int mercenaryMultiplyPerLife;
        [Space(5)] public bool isMultiplicateLifeAndLevel;
    }

    [System.Serializable]
    public class Buy
    {
        public int baseMultiplyArmy;
        public int baseMultiplyMercenary;
        [Space(5)] public int constMultiplicator;
    }

    [System.Serializable]
    public class LvlUp
    {
        public int armyLevelMultiplicator;
        [Space(5)] public int mercenaryLevelMultiplicator;
    }
}