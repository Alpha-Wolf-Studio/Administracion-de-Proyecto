using System.Collections;
using UnityEngine;

public class UnitStateShooting : MonoBehaviour
{
    private Unit unit;
    [SerializeField] private GameObject prefabBullet;
    
    private Transform enemyTransform;
    public Transform GetEnemyTransform() => enemyTransform;

    private void Awake()
    {
        unit = GetComponent<Unit>();
        unit.OnEnemyLocalizate += SetEnemy;
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
        if (!enemyTransform)
        {
            Collider[] coll = Physics.OverlapSphere(transform.position, unit.stats.radiusSight, unit.layerMaskInteraction);
            if (coll.Length > 0)
                enemyTransform = coll[0].transform;
            else
            {
                //Debug.LogWarning("No hay Enemigo.");
                return;
            }
        }

        GameObject bulletGameObject = Instantiate(prefabBullet, transform.position, Quaternion.identity, BulletParent.Get().GetTransform());
        Bullet bullet = bulletGameObject.GetComponent<Bullet>();
        bullet.SetAttributes(GetComponent<Unit>().layerMaskInteraction, unit.stats.damage, unit.stats.bulletSpeed);
        bulletGameObject.transform.LookAt(enemyTransform, Vector3.up);
    }
}