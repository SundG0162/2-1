using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStep05 : TutorialStep
{
    [SerializeField]
    private GameObject _boxObj;
    private bool _isActive = false;
    public override void Enter()
    {
        _boxObj.SetActive(true);
        _isActive = true;
        TutorialManager.Instance.SetMessage("�������� �پ� ���� Ÿ�� �޸� �� �ֽ��ϴ�.\n���ʿ� ���̴� �Ķ��� �ڽ����� �����غ�����.");
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
