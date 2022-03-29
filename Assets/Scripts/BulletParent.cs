
using UnityEngine;

public class BulletParent : MonoBehaviourSingleton<BulletParent>
{
    public Transform GetTransform() => transform;
}