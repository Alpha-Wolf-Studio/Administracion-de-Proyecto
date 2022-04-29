using System;
using System.Collections;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class Unit : MonoBehaviour
{
    public Action<float, float> OnTakeDamage;
    public Action OnDie;
    private UnitStateMoving unitStateMoving;
    private UnitStateShooting unitStateShooting;

    public enum State
    {
        Move,
        Move_To_Trench,
        On_Trench,
        Shoot,
        Shoot_On_Trench
    };

    private State state = State.Move;
    private float onTimeCheckEnemy = 0.2f;

    private UnitStats initialStats;
    public LayerMask layerMaskInteraction;
    public Action<Transform> OnEnemyLocalizate;
    public Action<Transform> OnTrenchLocalizate;
    public Action OnRemovedMovingTarget;
    public int signDirection = 1;

    [HideInInspector] public UnitStats stats;
    public State GetCurrentState() => state;

    private void Awake()
    {
        unitStateMoving = GetComponent<UnitStateMoving>();
        unitStateShooting = GetComponent<UnitStateShooting>();

        if (unitStateMoving) unitStateMoving.OnTargetPositionReached += LockInTrench;
    }

    private void Start()
    {
        StartCoroutine(CheckChangeState());
    }

    private IEnumerator CheckChangeState()
    {
        while (true)
        {
            Collider[] coll = Physics.OverlapSphere(transform.position, stats.radiusSight, layerMaskInteraction);
            if (coll.Length > 0)
            {
                ChooseColliderPriority(coll);
            }
            else
            {
                if (state != State.Move)
                {
                    if (stats.canMove)
                    {
                        ChangeState(State.Move);
                    }
                }
            }

            yield return new WaitForSeconds(onTimeCheckEnemy);
        }
    }

    private void ChooseColliderPriority(Collider[] coll)
    {


        Transform t = transform;

        for (int i = 0; i < coll.Length; i++)
        {
            var currentCollUnit = coll[i].GetComponent<Unit>();
            if (currentCollUnit)
            {
                if (state != State.Shoot && state != State.Shoot_On_Trench)
                {
                    if (stats.canShoot)
                    {
                        OnEnemyLocalizate?.Invoke(coll[i].transform);
                    }

                    if (state == State.On_Trench) ChangeState(State.Shoot_On_Trench);
                    else ChangeState(State.Shoot);
                }

                return;
            }

            var currentCollTrench = coll[i].GetComponent<Trench>();
            if (currentCollTrench)
            {
                if (state != State.Move_To_Trench && state != State.On_Trench && state != State.Shoot_On_Trench)
                {
                    var trenchFree = currentCollTrench.GetFreePosition(this);
                    if (trenchFree != null)
                    {
                        ChangeState(State.Move_To_Trench);
                        OnTrenchLocalizate?.Invoke(trenchFree.transform);
                    }
                }
            }
        }
    }

    private void LockInTrench() => ChangeState(State.On_Trench);

    private void ChangeState(State newState)
    {
        state = newState;
        switch (state)
        {
            case State.Shoot:
                if (stats.canMove) unitStateMoving.enabled = false;
                if (stats.canShoot) unitStateShooting.enabled = true;
                break;
            case State.Move:
                if (stats.canMove) unitStateMoving.enabled = true;
                if (stats.canShoot) unitStateShooting.enabled = false;
                break;
            case State.Move_To_Trench:
                if (stats.canMove) unitStateMoving.enabled = true;
                if (stats.canShoot) unitStateShooting.enabled = false;
                break;
            case State.On_Trench:
                if (stats.canMove) unitStateMoving.enabled = false;
                if (stats.canShoot) unitStateShooting.enabled = false;
                break;
            case State.Shoot_On_Trench:
                if (stats.canMove) unitStateMoving.enabled = false;
                if (stats.canShoot) unitStateShooting.enabled = true;
                break;
            default:
                Debug.Log("Error en el comportamiento de la unidad!!", gameObject);
                break;
        }
    }

    public void SetValues(UnitStats stats)
    {
        this.stats = stats;
        initialStats = stats;
    }

    public void TakeDamage(float damage)
    {
        stats.life -= damage;
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
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, Vector3.up, stats.radiusSight);
    }
#endif
}