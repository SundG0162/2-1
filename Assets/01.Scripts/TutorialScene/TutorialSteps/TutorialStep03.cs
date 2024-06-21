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
        TutorialManager.Instance.SetMessage("WASD를 눌러 움직일 수 있습니다.\n앞에 보이는 파란색 박스까지 도달해보세요.");
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
