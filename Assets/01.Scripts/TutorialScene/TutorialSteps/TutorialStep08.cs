using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStep08 : TutorialStep
{
    [SerializeField]
    private Door _door;
    public override void Enter()
    {
        TutorialManager.Instance.SetMessage("���� ��ư�� Ÿ�̸� ��ư�̸� ���� ���� ���� �ð� �ڿ� �����ϴ�.");
    }

    public override void UpdateStep()
    {
        if (_door.IsOpened)
        {
            TutorialManager.Instance.NextTutorial();
        }
    }
}
