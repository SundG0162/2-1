using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleButton : InteractableObject, IInteractableButton
{
    [SerializeField]
    private Door _door;

    public bool IsActive { get; set; } = false;

    protected override void Interact()
    {
        IsActive = !IsActive;
        if (_door != null)
            _door.ModifyOpenStatus(IsActive);
    }
}
