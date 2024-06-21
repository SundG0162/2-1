using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStep06 : TutorialStep
{
    private float _tutorialTime = 3f;
    private float _tutorialTimer = 0f;
    public override void Enter()
    {
        TutorialManager.Instance.SetMessage("추가로 ctrl키를 눌러 슬라이딩 할 수 있습니다.\n 속도가 더 빠릅니다만 방향 조작이 원활하지 않을 수 있습니다.");
    }

    public override void UpdateStep()
    {
        _tutorialTimer += Time.deltaTime;
        if (_tutorialTimer > _tutorialTime)
        {
            TutorialManager.Instance.NextTutorial();
        }
    }
}
