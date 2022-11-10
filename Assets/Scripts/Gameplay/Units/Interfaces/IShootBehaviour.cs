using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShootBehaviour
{
    void SpawnProjectile();
    void SetPrefabProjectile(Projectile proj);
}
