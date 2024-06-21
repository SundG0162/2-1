using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStep07 : TutorialStep
{
    [SerializeField]
    private Door _door;
    public override void Enter()
    {
        TutorialManager.Instance.SetMessage("�ʷ� ��ư�� ��� ��ư�̸� �ѹ� �� ������� ���� ���� �� �ֽ��ϴ�.");
    }

    public override void UpdateStep()
    {
        if(_door.IsOpened)
        {
            TutorialManager.Instance.NextTutorial();
        }
    }
}
