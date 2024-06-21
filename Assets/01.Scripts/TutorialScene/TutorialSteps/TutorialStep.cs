using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TutorialStep : MonoBehaviour
{
    public virtual void Enter() { }
    public virtual void UpdateStep() { }
    public virtual void Exit() { }
}
