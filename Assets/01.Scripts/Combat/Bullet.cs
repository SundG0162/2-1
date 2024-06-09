using ObjectPooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PoolableMono 
{
    private Vector3 _moveDir = Vector3.zero;
    private float _moveSpeed = 30f;

    private void Update()
    {
        transform.position += _moveDir * _moveSpeed * Time.deltaTime;
    }
    public void Fire(Vector3 dir, float speed)
    {
        _moveDir = dir;
        _moveSpeed = speed;
    }

    public override void ResetItem()
    {
        _moveDir = Vector3.zero;
        _moveSpeed = 0;
    }
}
