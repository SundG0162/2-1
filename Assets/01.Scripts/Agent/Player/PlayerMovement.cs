using UnityEngine;

public class PlayerMovement : AgentMovement
{
    private Player _player;

    [Header("Camera Settings")]
    [SerializeField]
    private Transform _playerMainCam;
    [SerializeField]
    private float _sensivityX = 8f;
    [SerializeField]
    private float _sensivityY = 4f;

    [SerializeField] private float _upperLookLimit = -70.0f, _lowerLookLimit = 70.0f;


    private float _xRotation = 0;
    private Vector2 _cameraRotDelta;

    private Vector3 _movement;

    private void Start()
    {
        _player = GetComponent<Player>();
    }
    public void SetCameraRotate(Vector2 delta)
    {
        _cameraRotDelta.x = delta.y * _sensivityX * Time.deltaTime;
        float rotateY = delta.x * _sensivityY * Time.deltaTime;
        transform.Rotate(0, rotateY, 0);
    }

    protected override void FixedUpdate()
    {
        _velocity = GetRotateVelocity();
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

    private Vector3 GetRotateVelocity()
    {
        Vector3 forward = transform.forward;
        forward.y = 0;
        Vector3 right = transform.right;
        right.y = 0;
        Vector3 velocity = forward * _movement.z + right * _movement.x;
        velocity.y = _velocity.y;
        return velocity;
    }

    private void CameraRotate()
    {
        SetCameraRotate(_player.PlayerInput.CamDelta);
        _xRotation -= _cameraRotDelta.x;

        _xRotation = Mathf.Clamp(_xRotation, _upperLookLimit, _lowerLookLimit);
        _playerMainCam.transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
    }
}