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
    public Action OnDamageRedirect;
    public LayerMask enemyMask;
    public LayerMask interactableMask;
    public int signDirection = 1;
    public LanesFlags AttackLaneFlags;
    public LanesFlags OwnLaneFlags;

    [SerializeField] private List<UnitBehaviour> unitBehaviours = default;
    [SerializeField] private List<SkinnedMeshRenderer> baseTroopMeshRenderers = default;

    public void SetBaseTroopMaterial(Material mat)
    {
        foreach (var rend in baseTroopMeshRenderers)
        {
            rend.material = mat;
        }
    }

    private Unit redirectDamageUnit = null;
    
    public Unit RedirectDamageUnit
    {
        get => redirectDamageUnit;
        set
        {
            redirectDamageUnit = value;
            
            if(redirectDamageUnit)
                OnDamageRedirect?.Invoke();
        }
    } 

    private Collider collider;

    [HideInInspector] public UnitStats stats;
    [HideInInspector] public UnitStats initialStats;

    private void Awake()
    {
        SetUnitBehaviours();
        collider = GetComponent<Collider>();
    }

    private IEnumerator Start()
    {
        while (gameObject)
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

    public void SetValues(UnitStats stats, int unitLevel)
    {
        this.stats = new UnitStats(stats, unitLevel);
        initialStats = new UnitStats(stats, unitLevel);
    }
    
    public void Heal(float amount)
    {
        stats.life += amount;
        if (stats.life > initialStats.life)
            stats.life = initialStats.life;
    }
    
    public void TakeDamage(float damage, UnitStats attackerStats)
    {
        if (RedirectDamageUnit)
        {
            RedirectDamageUnit.TakeDamage(damage, attackerStats);
            return;
        }
        
        if (!attackerStats.unitsDamageables.Exists(i => i == stats.unitType)) return;

        if (attackerStats.unitsPlusDamage.Exists(i => i == stats.unitType))
            damage *= 2;
        else if (attackerStats.unitsRestDamage.Exists(i => i == stats.unitType))
            damage /= 2;

        stats.life -= damage / stats.resistanceFactor;
        if (stats.life <= 0)
        {
            OnDie?.Invoke();
            if(collider)
                collider.enabled = false;
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