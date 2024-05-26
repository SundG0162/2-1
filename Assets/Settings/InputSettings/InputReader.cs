using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

[CreateAssetMenu(menuName = "InputReaderSO")]
public class InputReader : ScriptableObject, IPlayerActions
{
    public event Action<Vector2> OnMoveEvent;
    public event Action<Vector2> OnCameraRotateEvent;
    public event Action OnJumpEvent;
    public event Action OnCrouchDownEvent;
    public event Action OnCrouchUpEvent;
    public event Action OnAttackEvent;
    public Vector2 MoveInput {  get; private set; }

    private Controls _controls;

    public Vector2 CamDelta { get; private set; }

    private void OnEnable()
    {
        if(_controls == null)
        {
            _controls = new Controls();
            _controls.Player.SetCallbacks(this);
        }
        _controls.Player.Enable();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        OnAttackEvent?.Invoke();
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnCrouchDownEvent?.Invoke();
        if (context.canceled)
            OnCrouchUpEvent?.Invoke();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        OnJumpEvent?.Invoke();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        OnMoveEvent?.Invoke(input);
        MoveInput = input;
    }

    public void OnCameraRotate(InputAction.CallbackContext context)
    {
        CamDelta = context.ReadValue<Vector2>();
    }
}
