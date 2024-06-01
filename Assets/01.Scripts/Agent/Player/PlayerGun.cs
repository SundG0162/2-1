using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectPooling;

public class PlayerGun : MonoBehaviour
{
    private Player _player;

    [SerializeField]
    private Transform _gunTrm;
    private Transform _firePos;

    [SerializeField]
    Camera _visualCam;

    [SerializeField]
    private LayerMask _whatIsEnemy;
    [SerializeField]
    private LayerMask _whatIsObstacle;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _firePos = _gunTrm.Find("FirePos");
    }

    private void Start()
    {
        _player.PlayerInput.OnAttackEvent += HandleOnAttackEvent;
    }

    private void OnDestroy()
    {
        _player.PlayerInput.OnAttackEvent -= HandleOnAttackEvent;
    }

    private void HandleOnAttackEvent()
    {
        RaycastHit hit;
        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Camera.main.farClipPlane, _whatIsObstacle | _whatIsEnemy);
        BulletTrail trail = PoolManager.Instance.Pop(PoolingType.VFX_BulletTrail) as BulletTrail;

        trail.DrawTrail(_firePos.position, _visualCam.transform.forward * _visualCam.farClipPlane, 0.03f);
    }

}
