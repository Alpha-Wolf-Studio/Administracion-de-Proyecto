using System;
using UnityEngine;
using UnityEngine.Events;

public class UnitParticlesControl : MonoBehaviour
{

    public UnityEvent OnDieEvent;
    public UnityEvent OnTakeDamageEvent;
    public UnityEvent OnHealEvent;
    public UnityEvent OnReceiveBonusDamageEvent;
    public UnityEvent OnReceiveBonusRangeEvent;
    
    [SerializeField] private Unit unit;


    private void Awake()
    {
        unit.OnDie += delegate
        {
            OnDieEvent?.Invoke();
        };
        
        unit.OnTakeDamage += delegate(float initialLife, float maxLife)
        {
            OnTakeDamageEvent?.Invoke();
        };
        
        unit.OnHeal += delegate(float initialLife, float maxLife)
        {
            OnHealEvent?.Invoke();
        };
        
        unit.OnReceiveBonusDamage += delegate
        {
            OnReceiveBonusDamageEvent?.Invoke();
        };
        
        unit.OnReceiveBonusRange += delegate
        {
            OnReceiveBonusRangeEvent?.Invoke();
        };
    }
}
