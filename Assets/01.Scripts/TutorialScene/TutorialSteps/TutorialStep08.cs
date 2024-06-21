using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStep08 : TutorialStep
{
    [SerializeField]
    private Door _door;
    public override void Enter()
    {
        TutorialManager.Instance.SetMessage("빨간 버튼은 타이머 버튼이며 문을 연뒤 일정 시간 뒤에 닫힙니다.");
    }

    public override void UpdateStep()
    {
        if (_door.IsOpened)
        {
            TutorialManager.Instance.NextTutorial();
        }
    }
}
