using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualCameraStand : MonoBehaviour
{
    [SerializeField]
    private Transform _headTrm;

    [SerializeField]
    private float _followPosFactor = 0.9f;
    [SerializeField]
    private float _followAngleFactor = 0.9f;

    private Vector3 _prevParentPos;
    private Vector3 _prevParentAngle;

    void Start()
    {
        _prevParentPos = _headTrm.position;
        _prevParentAngle = _headTrm.eulerAngles;
        transform.position = _headTrm.position;
    }

    void Update()
    {
        /*Vector3 parentPosDelta = _headTrm.position - _prevParentPos;

        transform.position += parentPosDelta * _followPosFactor;

        Vector3 parentAngleDelta = _headTrm.eulerAngles - _prevParentAngle;
        Vector3 newAngle = transform.eulerAngles + parentAngleDelta * _followAngleFactor;

        transform.eulerAngles = newAngle;

        _prevParentPos = _headTrm.position;
        _prevParentAngle = _headTrm.eulerAngles;*/
    }

}
