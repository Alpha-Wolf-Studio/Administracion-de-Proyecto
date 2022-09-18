using UnityEngine;
using System;
using System.Linq;
using System.Collections;
public class UnitHideBehaviour : UnitBehaviour
{

    [SerializeField] private float hideAgainTime = 2f;
    [SerializeField] private float targetDistanceTolerance = .15f;
    [SerializeField] private Transform groundPosition = default;

    private Unit unit = default;
    private Rigidbody rb = default;

    private Collider[] trenchColliders;

    private Transform target = null;

    private bool hiding = false;

    private bool shouldGetOut = false;
    public void GetOut() => shouldGetOut = true;

    private void Awake()
    {
        unit = GetComponent<Unit>();
        rb = GetComponent<Rigidbody>();
    }

    public override void Execute()
    {
        if (!hiding)
        {
            Vector3 moveDirection = (target.position - groundPosition.position).normalized * (unit.stats.velocity * Time.deltaTime);
            rb.MovePosition(rb.position + moveDirection);
            transform.LookAt(transform.position + moveDirection);
            if (Vector3.Distance(target.position, groundPosition.position) < targetDistanceTolerance)
            {
                hiding = true;
                unit.stats.resistanceFactor = 2f;
                unit.stats.bonusRange = unit.stats.rangeAttack;
            }
        }
        else if (shouldGetOut)
        {
            hiding = false;
            target = null;
            StartCoroutine(CanHideAgainCoroutine());
        }

        OnMoving?.Invoke(!hiding);
        OnAttacking?.Invoke(false);
    }

    private IEnumerator CanHideAgainCoroutine() 
    {
        yield return new WaitForSeconds(hideAgainTime);
        shouldGetOut = false;
    }

    public override bool IsBehaviourExecutable()
    {
        if (shouldGetOut) return false;

        trenchColliders = Physics.OverlapSphere(transform.position, unit.stats.radiusSight, unit.interactableMask);
        trenchColliders = Array.FindAll(trenchColliders, IsTrenchValid).ToArray(); // SYSTEM ARRAY
        //trenchColliders = trenchColliders.Where(collider => IsEnemyValid(collider)).ToArray(); // LINQ

        if (trenchColliders.Length > 0 && target == null)
        {
            var trench = trenchColliders[0].GetComponent<Trench>();
            if (trench && trench.IsCoverageFree(gameObject.layer)) 
            {
                target = trench.GetFreePosition(unit);
            }
            else 
            {
                return false;
            }
        }
        return trenchColliders.Length > 0;
    }

    private bool IsTrenchValid(Collider collider) => collider != null && AnyFlagContained(collider.GetComponent<Unit>().OwnLaneFlags, unit.AttackLaneFlags);

    private bool AnyFlagContained(Enum me, Enum other)
    {
        return (Convert.ToInt32(me) & Convert.ToInt32(other)) != 0;
    }

}
