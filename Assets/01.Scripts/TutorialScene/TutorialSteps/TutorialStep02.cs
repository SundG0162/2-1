using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStep02 : TutorialStep
{
    [SerializeField]
    private Door _door;
    public override void Enter()
    {
        TutorialManager.Instance.SetMessage("��Ŭ���� ���� ���� �߻��� �� �ֽ��ϴ�.\n�� �տ� �ʷϻ� ��ư�� �� ���� �������.");
    }

    public override void UpdateStep()
    {
        if(_door.IsOpened)
        {
            TutorialManager.Instance.NextTutorial();
        }
    }
}
