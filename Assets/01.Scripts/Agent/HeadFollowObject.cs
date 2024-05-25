using UnityEngine;

public class HeadFollowObject : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField]
    private Transform _parentTrm;
    [SerializeField]
    private float _followPosFactor = 0.5f;
    [SerializeField]
    private float _followRotFactor = 0.5f;
    [SerializeField]
    private float _xRotOffset = 20f;

    private Vector3 _prevParentPos;
    private Quaternion _prevParentRot;

    void Start()
    {
        if (_parentTrm == null)
        {
            Debug.LogError("Parent Transform is not assigned.");
            return;
        }

        _prevParentPos = _parentTrm.position;
        _prevParentRot = _parentTrm.rotation;
    }

    void Update()
    {
        Vector3 deltaPos = _parentTrm.position - _prevParentPos;

        transform.position += deltaPos * _followPosFactor;

        Quaternion parentRotationDelta = _parentTrm.rotation * Quaternion.Inverse(_prevParentRot);
        transform.rotation = Quaternion.Slerp(Quaternion.identity, parentRotationDelta, _followRotFactor);
        transform.eulerAngles +=  new Vector3(_xRotOffset, 0, 0);
        _prevParentPos = _parentTrm.position;
        _prevParentRot = _parentTrm.rotation;
    }
}