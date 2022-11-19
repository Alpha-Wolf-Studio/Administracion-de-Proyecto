using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShootBehaviour
{
    Action OnReAttack { get; set; }
    bool CheckReAttack { get; set; }
    void SpawnProjectile();
    void SetPrefabProjectile(Projectile proj);
}
