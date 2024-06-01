using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectPooling;
using Random = UnityEngine.Random;
using UnityEditorInternal;

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


    [Header("Recoil Setting")]
    [SerializeField]
    private Transform _headTrm;
    [SerializeField]
    private float _recoilX, _recoilY, _recoilZ;
    [SerializeField]
    private float _returnSpeed;
    [SerializeField]
    private float _snappiness;

    private Vector3 _currentRot;
    private Vector3 _targetRot;
    

    


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
        Recoil();
        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Camera.main.farClipPlane, _whatIsObstacle | _whatIsEnemy);
        BulletTrail trail = PoolManager.Instance.Pop(PoolingType.VFX_BulletTrail) as BulletTrail;

        trail.DrawTrail(_firePos.position, _visualCam.transform.forward * _visualCam.farClipPlane, 0.03f);

        if(hit.collider != null)
        {
            if (hit.collider.TryGetComponent(out Health health))
            {
                health.GetDamage();
            }
        }
    }

    
    private void Update()
    {
        _targetRot = Vector3.Lerp(_targetRot, Vector3.zero, _returnSpeed * Time.deltaTime);
        _currentRot = Vector3.Slerp(_currentRot, _targetRot, _snappiness * Time.deltaTime);
        _headTrm.localRotation = Quaternion.Euler(_currentRot);
    }

    private void Recoil()
    {
        _targetRot = new Vector3(_recoilX, Random.Range(-_recoilY, _recoilY), Random.Range(-_recoilZ, _recoilZ));
    }

}
