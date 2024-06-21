using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDeadZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            TutorialManager.Instance.Restart();
        }
    }
}
