using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("???");
        if (other.TryGetComponent(out Player player))
        {
            if (StageManager.Instance.CurrentStage.enemyList.Count <= 0)
            {
                StageManager.Instance.NextStage();
            }
        }
    }
}
