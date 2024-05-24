using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

public class InputReader : ScriptableObject, IPlayerActions
{
    public event Action<Vector2> OnMoveEvent;
    public event Action OnJumpEvent;
    public event Action OnCrouchDownEvent;
    public event Action OnCrouchUpEvent;
    public event Action OnAttackEvent;

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
    }
}
