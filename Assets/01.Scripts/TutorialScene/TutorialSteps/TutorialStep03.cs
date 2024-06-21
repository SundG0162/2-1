using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStep03 : TutorialStep
{
    [SerializeField]
    private GameObject _boxObj;
    private bool _isActive = false;
    public override void Enter()
    {
        _isActive = true;
        TutorialManager.Instance.SetMessage("WASD�� ���� ������ �� �ֽ��ϴ�.\n�տ� ���̴� �Ķ��� �ڽ����� �����غ�����.");
    }

    public override void Exit()
    {
        _boxObj.SetActive(false);
        _isActive = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isActive) return;
        if (other.TryGetComponent(out Player player))
        {
            TutorialManager.Instance.NextTutorial();
        }
    }
}
