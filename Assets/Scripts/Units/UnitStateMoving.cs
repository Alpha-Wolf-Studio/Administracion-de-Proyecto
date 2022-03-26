using UnityEngine;
public class UnitStateMoving : MonoBehaviour
{
    private Unit unit;

    private void Awake()
    {
        unit = GetComponent<Unit>();
    }
    private void Update()
    {
        transform.position += new Vector3(unit.stats.velocity * Time.deltaTime * unit.signDirection, 0, 0);
    }
}