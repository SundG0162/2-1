using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerButton : InteractableObject, IInteractableButton
{
    [SerializeField]
    private float _closeTime = 2f;
    private float _closeTimer = 0;

    [SerializeField]
    private Door _door;

    public bool IsActive { get; set; } = false;

    protected override void Interact()
    {
        IsActive = true;
        _closeTimer = 0;
        _door.ModifyOpenStatus(true);
    }

    private void Update()
    {
        if (IsActive)
        {
            _closeTimer += Time.deltaTime;

            if(_closeTimer > _closeTime)
            {
                IsActive = false;
                _door.ModifyOpenStatus(false);
            }
        }
    }
}
