using UnityEngine;
public class UnitStateMoving : MonoBehaviour
{
    private Unit unit;
    [SerializeField] private Rigidbody rb;

    private void Awake()
    {
        unit = GetComponent<Unit>();
        rb.GetComponent<Rigidbody>();
    }
    private void Update()
    {
        Vector3 move = new Vector3(unit.stats.velocity * Time.deltaTime * unit.signDirection, 0, 0);
        rb.MovePosition(rb.position + move);
    }
}