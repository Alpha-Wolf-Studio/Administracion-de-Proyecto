using System;
using UnityEngine;

[Serializable]
public class UnitStats
{
    public UnitStats()
    {
    }

    public UnitStats(UnitStats stats)
    {
        nameUnit = stats.nameUnit;
        life = stats.life;
        damage = stats.damage;
        velocity = stats.velocity;
        radiusSight = stats.radiusSight;
        fireRate = stats.fireRate;
        bulletSpeed = stats.bulletSpeed;

        canMove = stats.canMove;
        canShoot = stats.canShoot;
    }

    public string nameUnit = "No name";
    public float life = 10;
    public float damage = 1;
    public float velocity = 2;
    public float radiusSight = 5;
    public float fireRate = 0.8f;
    public float bulletSpeed = 10f;
    [Space(10)] 
    public bool canMove = true;
    public bool canShoot = true;
}