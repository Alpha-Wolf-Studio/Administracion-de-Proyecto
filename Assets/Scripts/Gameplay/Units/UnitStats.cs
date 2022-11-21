using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UnitStats
{

    public UnitStats()
    {

    }

    public UnitStats(UnitStats unitStat, int unitLevel)
    {
        nameUnit = unitStat.nameUnit;
        idUnit = unitStat.idUnit;
        unitType = unitStat.unitType; // Sin uso
        movementType = unitStat.movementType;
        attackType = unitStat.attackType;
        unitsDamageables = unitStat.unitsDamageables; // Sin uso
        unitsPlusDamage = unitStat.unitsPlusDamage; // Sin uso
        unitsRestDamage = unitStat.unitsRestDamage; // Sin uso
        resistanceFactor = unitStat.resistanceFactor;
        bonusRange = unitStat.bonusRange;
        life = unitStat.life; // Mejorable
        damage = unitStat.damage; // Mejorable
        velocity = unitStat.velocity; // Mejorable
        radiusSight = unitStat.radiusSight; // Rango de Vision
        rangeAttack = unitStat.rangeAttack; // Mejorable        // Rango de Ataque
        fireRate = unitStat.fireRate; // Mejorable
        bulletSpeed = unitStat.bulletSpeed;

        canMove = unitStat.canMove;
        canShoot = unitStat.canShoot;

        tempCurrentShape = unitStat.tempCurrentShape;
        tempColor = unitStat.tempColor;
        
        SetStatsByLevel(unitLevel, idUnit);
    }

    public void ResetBonusStats()
    {
        resistanceFactor = 1f;
        bonusRange = 0f;
    }

    public string nameUnit = "No name";
    public int idUnit = 0;
    public UnitsType unitType; // Sin uso
    public MovementType movementType;
    public AttackType attackType;
    public List<UnitsType> unitsDamageables; // Sin uso
    public List<UnitsType> unitsPlusDamage; // Sin uso
    public List<UnitsType> unitsRestDamage; // Sin uso
    public float resistanceFactor = 1; // Mejorable
    public float bonusRange = 0; // Mejorable
    public float life = 10; // Mejorable
    public float damage = 1; // Mejorable
    public float velocity = 2; // Mejorable
    public float radiusSight = 5; // Rango de Vision
    public float rangeAttack = 5; // Mejorable        // Rango de Ataque
    public float fireRate = 0.8f; // Mejorable
    public float bulletSpeed = 10f;

    public int moneyPerLevel = 100;

    [Space(10)] public bool canMove = true;
    public bool canShoot = true;

    // Temp
    public CurrentShape tempCurrentShape = CurrentShape.Sphere;
    public Color tempColor = Color.black;
    // End Temp

    public void SetStatsByLevel(int currentUnitLevel, int idUnit)
    {
        life += UnitUpgradeScriptableObject.Instance.Life(idUnit, currentUnitLevel);
        damage += UnitUpgradeScriptableObject.Instance.Damage(idUnit, currentUnitLevel);
    }

    public float GetLifeLevel(int currentUnitLevel, int idUnit)
    {
        float newLife = life;
        newLife += UnitUpgradeScriptableObject.Instance.Life(idUnit, currentUnitLevel);

        return newLife;
    }

    public float GetDamageLevel(int currentUnitLevel, int idUnit)
    {
        float newDamage = damage;
        newDamage += UnitUpgradeScriptableObject.Instance.Damage(idUnit, currentUnitLevel);

        return newDamage;
    }
}

public enum UnitsType   // No modificar el orden, agregar nuevos debajo
{
    Edification,
    Infantry,
    Artillery,
    Aerial,
    Armored
}
public enum AttackType   // No modificar el orden, agregar nuevos debajo
{
    Shooting,
    Grenade,
    Missile,
    Bomb
}
public enum MovementType   // No modificar el orden, agregar nuevos debajo
{
    None,
    Rect
}
public enum CurrentShape   // No modificar luego se elimina y cada prefab tiene su Mesh y sprite
{
    Sphere,
    Cube,
    Capsule,
    Cylinder
}