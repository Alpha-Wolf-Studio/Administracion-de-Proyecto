using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UnitStats
{
    public string nameUnit = "No name";
    public UnitsType unitType;                              // Sin uso
    public MovementType movementType;                       
    public AttackType attackType;                           
    public List<UnitsType> unitsDamageables;                // Sin uso
    public List<UnitsType> unitsPlusDamage;                 // Sin uso
    public List<UnitsType> unitsRestDamage;                 // Sin uso
    public float life = 10;             // Mejorable
    public float damage = 1;            // Mejorable
    public float velocity = 2;          // Mejorable
    public float radiusSight = 5;                           // Rango de Vision
    public float rangeAttack = 5;       // Mejorable        // Rango de Ataque
    public float fireRate = 0.8f;       // Mejorable
    public float bulletSpeed = 10f;
    [Space(10)] 
    public bool canMove = true;
    public bool canShoot = true;

    public CurrentShape tempCurrentShape = CurrentShape.Sphere;
    public Color tempColor = Color.black;
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