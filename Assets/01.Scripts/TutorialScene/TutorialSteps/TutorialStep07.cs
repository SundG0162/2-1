using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStep07 : TutorialStep
{
    [SerializeField]
    private Door _door;
    public override void Enter()
    {
        TutorialManager.Instance.SetMessage("초록 버튼은 토글 버튼이며 한번 더 누를경우 문을 닫을 수 있습니다.");
    }

    public override void UpdateStep()
    {
        if(_door.IsOpened)
        {
            TutorialManager.Instance.NextTutorial();
        }
    }
}
