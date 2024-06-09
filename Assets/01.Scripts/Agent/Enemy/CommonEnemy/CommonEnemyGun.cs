using ObjectPooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonEnemyGun : AgentGun
{
    public override void Fire(Vector3 dir)
    {
        Bullet bullet = PoolManager.Instance.Pop(PoolingType.Combat_Bullet) as Bullet;
        bullet.transform.position = _firePosTrm.position;
        bullet.Fire(dir, 10);
    }
}
