using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleButton : InteractableObject
{
    private bool _isOpen = false;
    [SerializeField]
    private Door _door;
    protected override void Interact()
    {
        _isOpen = !_isOpen;
        _door.ModifyOpenStatus(_isOpen);
    }
}
