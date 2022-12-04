using System.Collections.Generic;
using PrivateClassUpgrades;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrades Progression", menuName = "Upgrades/Upgrades Progression", order = 2)]
public class UpgradeProgression : ScriptableObject
{
    public List<Buy> buy = new List<Buy>();
    public List<Expand> expand = new List<Expand>();
    public List<LvlUp> lvlUp = new List<LvlUp>();
    public List<Heal> heal = new List<Heal>();
}

namespace PrivateClassUpgrades
{
    [System.Serializable]
    public class Buy
    {
        public string name = "";
        public int baseCostArmy;
        public int baseCostMercenary;
    }

    [System.Serializable]
    public class Expand
    {
        public string name = "";
        public int baseCostArmy;
    }

    [System.Serializable]
    public class LvlUp
    {
        public string name = "";
        public int baseCost;
    }

    [System.Serializable]
    public class Heal
    {
        public string name = "";
        public int baseCostArmy;
    }
}