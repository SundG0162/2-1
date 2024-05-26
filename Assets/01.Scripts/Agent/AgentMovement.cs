using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AgentMovement : MonoBehaviour
{
    [SerializeField]
    private float _gravity = -9.8f;

    protected CharacterController _characterController;

    protected Vector3 _velocity;
    public Vector3 Velocity => _velocity;
    private float _verticalVelocity;

    public bool IsGround => _characterController.isGrounded;

    public void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    public abstract void SetMovement(Vector3 movement);

    public virtual void StopImmediately()
    {
        _velocity = Vector3.zero;
    }

    protected virtual void FixedUpdate()
    {
        ApplyGravity();
        Move();
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
    }
}
