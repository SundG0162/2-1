using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageStartBtn : InteractableObject, IInteractableButton
{
    [SerializeField]
    private Door _door;
    public bool IsActive { get; set; }

    protected override void Interact() 
    {
        IsActive = !IsActive;
        if (_door != null)
            _door.ModifyOpenStatus(IsActive);
        GameManager.Instance.StartTimer();
    }

}
