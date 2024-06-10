using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private Transform _upDoor, _downDoor, _leftDoor, _rightDoor;
    private Vector3 _upOriginPos, _downOriginPos, _leftOriginPos, _rightOriginPos;

    private BoxCollider _collider;


    private Sequence _openSeq;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();

        _upOriginPos = _upDoor.position;
        _downOriginPos = _downDoor.position;
        _leftOriginPos = _leftDoor.position;
        _rightOriginPos = _rightDoor.position;
    }
    public void ModifyOpenStatus(bool value)
    {
        if (_openSeq != null && _openSeq.IsActive())
        {
            _openSeq.Kill();
        }
        _openSeq = DOTween.Sequence();

        _collider.enabled = !value;

        if (value)
        {
            _openSeq.Append(_leftDoor.DOLocalMoveX(6, 0.6f));
            _openSeq.Join(_rightDoor.DOLocalMoveX(-6, 0.6f));
            _openSeq.Join(_upDoor.DOLocalMoveY(6, 0.6f));
            _openSeq.Join(_downDoor.DOLocalMoveY(-6, 0.6f));
        }
        else
        {
            _openSeq.Append(_leftDoor.DOMove(_leftOriginPos, 0.6f));
            _openSeq.Join(_rightDoor.DOMove(_rightOriginPos, 0.6f));
            _openSeq.Join(_upDoor.DOMove(_upOriginPos, 0.6f));
            _openSeq.Join(_downDoor.DOMove(_downOriginPos, 0.6f));
        }
    }
}
