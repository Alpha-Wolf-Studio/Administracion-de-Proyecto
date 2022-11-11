using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EnemySpawnerTrigger : MonoBehaviour
{

    [SerializeField] private int alliesLayer = 7;
    public Action OnTriggered;

    private void Awake()
    {
        Collider col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == alliesLayer)
            OnTriggered?.Invoke();
    }
}
