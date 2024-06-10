using UnityEngine;
using System.Collections;
using DG.Tweening;

public class PlayerMovement : AgentMovement
{
    private Player _player;

    [Header("Camera Settings")]
    [SerializeField]
    private Transform _visualCamTrm;
    [SerializeField]
    private Transform _playerMainCam;
    [SerializeField]
    private float _sensivityX = 8f, _sensivityY = 4f;
    [SerializeField]
    private float _upperLookLimit = -70.0f, _lowerLookLimit = 70.0f;

    [Header("Wall Check Settings")]
    [SerializeField]
    private Transform _wallCheckerTrm;
    [SerializeField]
    private LayerMask _whatIsWall;

    [HideInInspector]
    public Vector3 defaultMainCamLocalPos;


    private bool _lockRotateXAxis = false;
    private bool _forwardMovement = true;
    private bool _isTeleporting = false;
    private float _xRotation = 0;
    private Vector2 _cameraRotDelta;

    private Vector3 _movement;


    private void Start()
    {
        _player = GetComponent<Player>();
        defaultMainCamLocalPos = _playerMainCam.localPosition;
    }

    public void ModifyLockCameraXAxisRotate(bool active)
    {
        _lockRotateXAxis = active;
    }

    public void SetPlayerRotate(Vector2 delta)
    {
        _cameraRotDelta.x = delta.y * _sensivityX * Time.deltaTime;
        float rotateY = delta.x * _sensivityY * Time.deltaTime;
        transform.Rotate(0, rotateY, 0);
    }

    protected override void FixedUpdate()
    {
        if (_isTeleporting) return;
        IsGround = _characterController.isGrounded;
        if (_forwardMovement)
            _velocity = GetRotateVelocity() * _player.moveSpeed;
        else
            _velocity = _movement * _player.moveSpeed;
        base.FixedUpdate();
    }

    private void LateUpdate()
    {
        CameraRotate();
    }

    public void Teleport(Vector3 pos)
    {
        StopImmediately();
        _isTeleporting = true;
        transform.position = pos;
        StartCoroutine(DelayMovement());
    }

    private IEnumerator DelayMovement()
    {
        yield return new WaitForSeconds(0.5f);
        _isTeleporting = false;
    }



    public override void SetMovement(Vector3 movement)
    {
        _movement = movement * Time.fixedDeltaTime;
    }

    public Vector3 GetRotateVelocity()
    {
        Vector3 forward = transform.forward;
        forward.y = 0;
        Vector3 right = transform.right;
        right.y = 0;
        Vector3 velocity = forward * _movement.z + right * _movement.x;
        velocity.y = _velocity.y;
        return velocity;
    }

    public void ModifyForwardMovement(bool active)
    {
        _forwardMovement = active;
    }

    private Tween _moveCamTween = null;
    public void LocalMoveMainCam(Vector3 pos, float duration, Ease ease = Ease.OutSine)
    {
        if (_moveCamTween != null && _moveCamTween.IsActive()) 
            _moveCamTween.Kill();
        _moveCamTween = _playerMainCam.DOLocalMove(pos, duration).SetEase(ease);
    }

    private Tween _tiltCamTween = null;
    public void TileMainCam(float value, float duration)
    {
        if (_tiltCamTween != null && _tiltCamTween.IsActive())
            _tiltCamTween.Kill();
        _tiltCamTween = _playerMainCam.DOLocalRotate(new Vector3(0,0,value), duration);
    }

    public void ModifyFollowHeadCam(bool active)
    {
        if (active)
        {
            _playerMainCam.SetParent(_visualCamTrm);
        }
        else
        {
            _playerMainCam.SetParent(_player.transform);
            _playerMainCam.DOLocalMove(defaultMainCamLocalPos, 0.2f);
        }
    }

    public bool CheckWall(out RaycastHit wallHitInfo, out bool isLeft)
    {
        RaycastHit rightHit;
        RaycastHit leftHit;
        float wallCheckDistance = 0.7f;
        bool wallRight = Physics.Raycast(new Ray(_wallCheckerTrm.position, transform.right), out rightHit, wallCheckDistance, _whatIsWall);
        bool wallLeft = Physics.Raycast(new Ray(_wallCheckerTrm.position, -transform.right), out leftHit, wallCheckDistance, _whatIsWall);
        wallHitInfo = wallRight ? rightHit : leftHit;
        isLeft = wallLeft;
        return wallRight || wallLeft;
    }

    private void CameraRotate()
    {
        SetPlayerRotate(_player.PlayerInput.CamDelta);
        if (_lockRotateXAxis) return;
        _xRotation -= _cameraRotDelta.x;

        _xRotation = Mathf.Clamp(_xRotation, _upperLookLimit, _lowerLookLimit);
        _playerMainCam.transform.localRotation = Quaternion.Euler(_xRotation, 0, _playerMainCam.transform.localEulerAngles.z);
    }
}