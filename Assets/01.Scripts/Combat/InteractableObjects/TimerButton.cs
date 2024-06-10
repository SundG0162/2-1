using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerButton : InteractableObject
{
    [SerializeField]
    private float _closeTime = 2f;
    private float _closeTimer = 0;
    private bool _isActive = false;

    [SerializeField]
    private Door _door;

    protected override void Interact()
    {
        _isActive = true;
        _closeTimer = 0;
        _door.ModifyOpenStatus(true);
    }

    private void Update()
    {
        if (_isActive)
        {
            _closeTimer += Time.deltaTime;

            if(_closeTimer > _closeTime)
            {
                _isActive = false;
                _door.ModifyOpenStatus(false);
            }
        }
    }
}
