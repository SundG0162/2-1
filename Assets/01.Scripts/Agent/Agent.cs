using System;
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

    public Coroutine StartDelayCallBack(float delayTime, Action CallBack)
    {
        return StartCoroutine(DelayCoroutine(delayTime, CallBack));
    }

    protected IEnumerator DelayCoroutine(float delayTime, Action CallBack)
    {
        yield return new WaitForSeconds(delayTime);
        CallBack?.Invoke();
    }
}
