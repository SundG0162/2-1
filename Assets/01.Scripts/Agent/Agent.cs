using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Agent : MonoBehaviour
{
    public bool CanStateChangeable { get; protected set; } = true;

    #region Components
    public Animator AnimatorCompo { get; protected set; }
    #endregion

    protected virtual void Awake()
    {
        Transform visualTrm = transform.Find("Visual");
        AnimatorCompo = visualTrm.GetComponent<Animator>();
    }

    public virtual void Attack() { }
}
