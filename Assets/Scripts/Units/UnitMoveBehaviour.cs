using UnityEngine;

public class UnitMoveBehaviour : UnitBehaviour
{

    private Unit unit = default;
    private Rigidbody rb = default;

    private void Awake()
    {
        unit = GetComponent<Unit>();
        rb = GetComponent<Rigidbody>();
    }

    public override void Execute()
    {
        Vector3 moveDirection = new Vector3(unit.stats.velocity * Time.deltaTime * unit.signDirection, 0, 0);
        rb.MovePosition(rb.position + moveDirection);
    }

    public override bool IsBehaviourExecutable()
    {
        return true;
    }




}
