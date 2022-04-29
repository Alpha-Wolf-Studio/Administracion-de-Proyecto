using UnityEngine;
public class UnitStateMoving : MonoBehaviour
{
    private Unit unit;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float targetDistanceTolerance = .15f;

    public System.Action OnTargetPositionReached;

    [SerializeField] private Transform groundPosition;
    private Transform target = null;

    public Transform GetTarget() => target;

    private void Awake()
    {
        unit = GetComponent<Unit>();
        rb.GetComponent<Rigidbody>();
        unit.OnTrenchLocalizate += ChangeTarget;
    }
    private void Update()
    {
        Vector3 moveDirection;
        if (target == null)
        {
            moveDirection = new Vector3(unit.stats.velocity * Time.deltaTime * unit.signDirection, 0, 0);
        }
        else
        {
            moveDirection = (target.position - groundPosition.position).normalized * (unit.stats.velocity * Time.deltaTime);

            if(Vector3.Distance(target.position, groundPosition.position) < targetDistanceTolerance) 
            {
                target = null;
                OnTargetPositionReached?.Invoke();
            }
        }

        Vector3 move = moveDirection;
        rb.MovePosition(rb.position + move);
    }

    private void ChangeTarget(Transform target) => this.target = target;
    private void RemoveTarget() => target = null;

}