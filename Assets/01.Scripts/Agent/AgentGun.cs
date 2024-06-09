using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AgentGun : MonoBehaviour
{
    [SerializeField]
    protected Transform _firePosTrm;

    public abstract void Fire(Vector3 dir);
}
