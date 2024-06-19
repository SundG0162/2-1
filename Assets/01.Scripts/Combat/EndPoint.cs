using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    private float _delayTime = 0.6f;
    private float _delayTimer = 0;
    private bool _active = false;

    private void Update()
    {
        if (_active) return;
        _delayTimer += Time.deltaTime;
        if (_delayTimer > _delayTime)
        {
            _active = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!_active) return;
        if (other.TryGetComponent(out Player player))
        {
            if (StageManager.Instance.CurrentStage.enemyList.Count <= 0)
            {
                StageManager.Instance.NextStage();
            }
        }
    }
}
