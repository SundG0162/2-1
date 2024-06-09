using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonEnemyAnimationEndTrigger : MonoBehaviour
{
    [SerializeField]
    private CommonEnemy _enemy;

    private void AnimationEndTrigger()
    {
        _enemy.StateMachine.CurrentState.AnimationEndTrigger();
    }
}
