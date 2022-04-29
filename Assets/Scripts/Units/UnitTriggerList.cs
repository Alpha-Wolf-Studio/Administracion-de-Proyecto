using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitTriggerList : MonoBehaviour
{
    public Action OnNewEnemy;
    [SerializeField] private Unit unit;

    public enum TypeVision
    {
        Sight,
        Attack
    }

    [SerializeField] private TypeVision typeVision;
    [SerializeField] private List<Transform> objetives = new List<Transform>();

    private void Start()
    {
        if (typeVision == TypeVision.Sight)
            transform.localScale = new Vector3(unit.stats.radiusSight, 1, 1);
        if (typeVision == TypeVision.Attack)
            transform.localScale = new Vector3(unit.stats.rangeAttack, 1, 1);
    }
    private void OnTriggerEnter(Collider other)
    {
        Unit otherUnit = other.GetComponent<Unit>();
        if (otherUnit)
        {
            if (unit.gameObject.layer == other.gameObject.layer) 
                return;

            OnNewEnemy?.Invoke();
            objetives.Add(other.transform);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Unit>())
        {
            if (unit.gameObject.layer == other.gameObject.layer)
                return;

            objetives.Remove(other.transform);
        }
    }
    public Transform GetObjetiveClosestTarget()
    {
        Vector3 unitPosition = unit.transform.position;
        Transform objetive = null;
        float minDistance = float.MaxValue;

        for (int i = 0; i < objetives.Count; i++)
        {
            float currentDistance = Vector3.Distance(unitPosition, objetives[i].position);
            if (currentDistance < minDistance)
            {
                objetive = objetives[i];
            }
        }
        return objetive;
    }
}