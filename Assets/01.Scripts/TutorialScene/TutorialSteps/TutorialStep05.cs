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
        TutorialManager.Instance.SetMessage("벽쪽으로 붙어 벽을 타고 달릴 수 있습니다.\n앞쪽에 보이는 파란색 박스까지 도달해보세요.");
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
