using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStep01 : TutorialStep
{
    private float _tutorialTime = 2f;
    private float _tutorialTimer = 0f;
    public override void Enter()
    {
        TutorialManager.Instance.SetMessage("�ȳ��ϼ���? Ʃ�丮���� �����ϰڽ��ϴ�.");
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
