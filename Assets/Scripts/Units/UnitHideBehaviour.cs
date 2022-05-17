using UnityEngine;
using System.Collections;
public class UnitHideBehaviour : UnitBehaviour
{

    [SerializeField] private float hideAgainTime = 2f;
    [SerializeField] private float targetDistanceTolerance = .15f;
    [SerializeField] private Transform groundPosition = default;

    private Unit unit = default;
    private Rigidbody rb = default;

    private Collider[] trenchColliders = new Collider[5]; //Puse 5 para que sobre en caso de una nueva utilidad ya que estoy reutilizando el array, con tener 1 ya alcanza.

    private Transform target = null;
    
    private int trenchsInSight = 0;

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
            if (Vector3.Distance(target.position, groundPosition.position) < targetDistanceTolerance)
            {
                hiding = true;
            }
        }
        else if (shouldGetOut)
        {
            hiding = false;
            target = null;
            StartCoroutine(CanHideAgainCoroutine());
        }

        OnMoved?.Invoke(!hiding);
    }

    private IEnumerator CanHideAgainCoroutine() 
    {
        yield return new WaitForSeconds(hideAgainTime);
        shouldGetOut = false;
    }

    public override bool IsBehaviourExecutable()
    {
        if (shouldGetOut) return false;
        trenchsInSight = Physics.OverlapSphereNonAlloc(transform.position, unit.stats.radiusSight, trenchColliders, unit.interactableMask);
        if (trenchsInSight > 0 && target == null)
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
        return trenchsInSight > 0;
    }
}
