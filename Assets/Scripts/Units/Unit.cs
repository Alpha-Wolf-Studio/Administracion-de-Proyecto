using System;
using System.Collections;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public abstract class Unit : MonoBehaviour
{
    private UnitStateMoving unitStateMoving;
    private UnitStateShooting unitStateShooting;
    private enum State { Move, Shoot };
    private State state = State.Move;
    private float onTimeCheckEnemy = 0.2f;

    public UnitStats stats;
    public LayerMask layerMaskEnemy;
    public Action<Transform> onEnemyLocalizate;
    public int signDirection = 1;
    private void Awake()
    {
        unitStateMoving = GetComponent<UnitStateMoving>();
        unitStateShooting = GetComponent<UnitStateShooting>();
    }
    private void Start()
    {
        StartCoroutine(CheckChangeState());
    }
    private IEnumerator CheckChangeState()
    {
        while (true)
        {
            Collider[] coll = Physics.OverlapSphere(transform.position, stats.radiusSight, layerMaskEnemy);
            if (coll.Length > 0)
            {
                if (state != State.Shoot)
                {
                    Debug.Log("Empieza a disparar.", gameObject);
                    onEnemyLocalizate?.Invoke(coll[0].transform);
                    ChangeState(State.Shoot);
                }
            }
            else
            {
                if (state != State.Move)
                {
                    Debug.Log("Empieza a moverse.", gameObject);
                    ChangeState(State.Move);
                }
            }

            yield return new WaitForSeconds(onTimeCheckEnemy);
        }
    }
    private void ChangeState(State newState)
    {
        state = newState;
        switch (state)
        {
            case State.Shoot:
                unitStateMoving.enabled = false;
                unitStateShooting.enabled = true;
                break;
            case State.Move:
                unitStateMoving.enabled = true;
                unitStateShooting.enabled = false;
                break;
            default:
                Debug.Log("Error en el comportamiento de la unidad!!", gameObject);
                break;
        }
    }
    public void SetValues(UnitStats stats)
    {
        this.stats = stats;
    }
    public void RecibeDamage(int damage)
    {
        stats.life -= damage;
        if (stats.life < 1)
        {
            Destroy(gameObject);
        }
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, Vector3.up, stats.radiusSight);
    }
#endif
}