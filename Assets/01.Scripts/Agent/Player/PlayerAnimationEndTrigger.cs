using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEndTrigger : MonoBehaviour
{
    [SerializeField]
    private Player _player;

    private void AnimationEndTrigger()
    {
        _player.StateMachine.CurrentState.AnimationEndTrigger();
    }
}
