using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class Unit : MonoBehaviour
{
    public Action<float, float> OnTakeDamage;
    public Action OnDie;
    public LayerMask enemyMask;
    public LayerMask interactableMask;
    public int signDirection = 1;

    [SerializeField] private List<UnitBehaviour> unitBehaviours = default;

    private UnitStats initialStats;

    [HideInInspector] public UnitStats stats;

    private void Awake()
    {
        SetUnitBehaviours();
    }

    private IEnumerator Start()
    {
        while (stats.life > 0)
        {
            foreach (var behaviour in unitBehaviours)
            {
                if (behaviour.IsBehaviourExecutable())
                {
                    behaviour.Execute();
                    break;
                }
            }

            yield return null;
        }
    }

    private void SetUnitBehaviours() 
    {
        var behavioursArray = GetComponents<UnitBehaviour>();
        if (behavioursArray.Length > 0)
        {
            unitBehaviours = new List<UnitBehaviour>(behavioursArray.Length);
            unitBehaviours.Clear();
            foreach (var behaviour in behavioursArray)
            {
                unitBehaviours.Add(behaviour);
            }
            unitBehaviours.Sort((a, b) => b.Priority.CompareTo(a.Priority));
        }
    }

    public void SetValues(UnitStats stats)
    {
        this.stats = new UnitStats(stats);
        initialStats = new UnitStats(stats);
    }

    public void TakeDamage(float damage)
    {
        stats.life -= damage / stats.bonusResistance;
        if (stats.life < 0)
        {
            OnDie?.Invoke();
            Destroy(gameObject);
            return;
        }

        OnTakeDamage?.Invoke(stats.life, initialStats.life);
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.color = Color.yellow;
        Handles.DrawWireDisc(transform.position, Vector3.up, stats.radiusSight);
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, Vector3.up, stats.rangeAttack);
    }
#endif
}