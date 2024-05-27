using UnityEngine;

public class PlayerMovement : AgentMovement
{
    private Player _player;

    [Header("Camera Settings")]
    [SerializeField]
    private Transform _visualCamTrm;
    [SerializeField]
    private Transform _playerMainCam;
    [SerializeField]
    private float _sensivityX = 8f;
    [SerializeField]
    private float _sensivityY = 4f;
    private Vector3 _defaultMainCamLocalPos;

    [SerializeField] private float _upperLookLimit = -70.0f, _lowerLookLimit = 70.0f;

    private bool _canRotateVelocity = true;
    private bool _lockRotateXAxis = false;
    private float _xRotation = 0;
    private Vector2 _cameraRotDelta;
    private Vector3 _facingDirection;

    private Vector3 _movement;

    private void Start()
    {
        _player = GetComponent<Player>();
        _defaultMainCamLocalPos = _playerMainCam.localPosition;
    }

    public void ModifyLockCameraXAxisRotate(bool active)
    {
        _lockRotateXAxis = active;
    }

    public void SetCameraRotate(Vector2 delta)
    {
        _cameraRotDelta.x = delta.y * _sensivityX * Time.deltaTime;
        float rotateY = delta.x * _sensivityY * Time.deltaTime;
        transform.Rotate(0, rotateY, 0);
    }

    protected override void FixedUpdate()
    {
        _velocity = GetRotateVelocity() * _player.moveSpeed;
        base.FixedUpdate();
    }

    private void LateUpdate()
    {
        CameraRotate();
    }

    public override void SetMovement(Vector3 movement)
    {
        _movement = movement * Time.fixedDeltaTime;
    }

    public Vector3 GetRotateVelocity()
    {
        if (_canRotateVelocity)
        {
            Vector3 forward = transform.forward;
            forward.y = 0;
            Vector3 right = transform.right;
            right.y = 0;
            Vector3 velocity = forward * _movement.z + right * _movement.x;
            velocity.y = _velocity.y;
            _facingDirection = velocity;
            return velocity;
        }
        else
            return _facingDirection;
        
    }

    public void ModifyRotateVelocity(bool active)
    {
        _canRotateVelocity = active;
    }

    public void ModifyFollowHeadCam(bool active)
    {
        if(active)
        {
            _playerMainCam.SetParent(_visualCamTrm);
        }
        else
        {
            _playerMainCam.SetParent(_player.transform);
            _playerMainCam.localPosition = _defaultMainCamLocalPos;
        }
    }

    private void CameraRotate()
    {
        SetCameraRotate(_player.PlayerInput.CamDelta);
        if (_lockRotateXAxis) return;
        _xRotation -= _cameraRotDelta.x;

        _xRotation = Mathf.Clamp(_xRotation, _upperLookLimit, _lowerLookLimit);
        _playerMainCam.transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
    }
}