using System.Collections;
using UnityEngine;

public class UnitStateShooting : MonoBehaviour
{
    private Unit unit;
    [SerializeField] private GameObject prefabBullet;
    [SerializeField] private Transform enemyTransform;
    private void Awake()
    {
        unit = GetComponent<Unit>();
        unit.onEnemyLocalizate += SetEnemy;
    }
    private void OnEnable()
    {
        StartCoroutine(Shooting());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    void SetEnemy(Transform enemyTransform)
    {
        this.enemyTransform = enemyTransform;
    }
    IEnumerator Shooting()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(unit.stats.fireRate);
        }
    }
    void Shoot()
    {
        Debug.Log("Dispara", gameObject);
        if (!enemyTransform)
        {
            Collider[] coll = Physics.OverlapSphere(transform.position, unit.stats.radiusSight, unit.layerMaskEnemy);
            if (coll.Length > 0)
                enemyTransform = coll[0].transform;
            else
            {
                //Debug.LogWarning("No hay Enemigo.");
                return;
            }
        }

        GameObject bulletGameObject = Instantiate(prefabBullet, transform.position, Quaternion.identity, transform);
        Bullet bullet = bulletGameObject.GetComponent<Bullet>();
        bullet.SetAttributes(GetComponent<Unit>().layerMaskEnemy, unit.stats.damage);
        bulletGameObject.transform.LookAt(enemyTransform, Vector3.up);
    }
}