using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartButton : InteractableObject, IInteractableButton
{
    public bool IsActive { get; set; }

    protected override void Interact() 
    {
        GameManager.Instance.GameStart();
    }

}
