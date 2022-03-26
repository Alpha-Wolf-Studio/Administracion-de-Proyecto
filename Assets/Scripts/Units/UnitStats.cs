using System;
[Serializable]
public class UnitStats
{
    public UnitStats(UnitStats stats)
    {
        nameUnit = stats.nameUnit;
        life = stats.life;
        damage = stats.damage;
        velocity = stats.velocity;
        radiusSight = stats.radiusSight;
        fireRate = stats.fireRate;
    }
    public string nameUnit = "No name";
    public int life = 10;
    public int damage = 1;
    public float velocity = 2;
    public float radiusSight = 5;
    public float fireRate = 0.8f;
}