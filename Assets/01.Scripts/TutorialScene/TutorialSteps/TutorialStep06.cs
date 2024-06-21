using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStep06 : TutorialStep
{
    private float _tutorialTime = 3f;
    private float _tutorialTimer = 0f;
    public override void Enter()
    {
        TutorialManager.Instance.SetMessage("�߰��� ctrlŰ�� ���� �����̵� �� �� �ֽ��ϴ�.\n �ӵ��� �� �����ϴٸ� ���� ������ ��Ȱ���� ���� �� �ֽ��ϴ�.");
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
