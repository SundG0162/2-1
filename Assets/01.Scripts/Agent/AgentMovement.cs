using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AgentMovement : MonoBehaviour
{
    [SerializeField]
    private float _gravity = -9.8f;

    protected CharacterController _characterController;

    private Transform _visualTrm;

    [SerializeField]
    protected Vector3 _velocity;
    public Vector3 Velocity => _velocity;
    private float _verticalVelocity;
    protected Quaternion _targetRotation;

    public bool IsGround => _characterController.isGrounded;

    public void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _visualTrm = transform.Find("Visual");
    }

    public abstract void SetMovement(Vector3 movement);
    public void SetVelocity(Vector3 velocity)
    {
        _velocity = velocity;
        _verticalVelocity = velocity.y;
    }

    public virtual void StopImmediately(bool withYAxis = false)
    {
        SetMovement(Vector3.zero);
        if (withYAxis)
            _velocity = Vector3.zero;
        else
        {
            _velocity = new Vector3(0, _velocity.y, 0);
        }
    }

    protected virtual void FixedUpdate()
    {
        ApplyGravity();
        ApplyRotation();
        Move();
    }

    private void ApplyRotation()
    {
        float rotationSpeed = 8f;
        _visualTrm.rotation = Quaternion.Lerp(_visualTrm.rotation, _targetRotation, Time.fixedDeltaTime * rotationSpeed);
    }

    private void ApplyGravity()
    {
        if (IsGround && _verticalVelocity <= 0)
        {
            _verticalVelocity = -0.03f;
        }
        else
        {
            _verticalVelocity += _gravity * Time.fixedDeltaTime;
        }
        _velocity.y = _verticalVelocity;
    }

    protected virtual void Move()
    {
        _characterController.Move(_velocity);
        if (_velocity.magnitude > 0.1f)
        {
            //_targetRotation = Quaternion.LookRotation(_velocity);
        }
    }
}
