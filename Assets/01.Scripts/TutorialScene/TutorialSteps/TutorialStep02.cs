using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStep02 : TutorialStep
{
    [SerializeField]
    private Door _door;
    public override void Enter()
    {
        TutorialManager.Instance.SetMessage("좌클릭을 눌러 총을 발사할 수 있습니다.\n문 앞에 초록색 버튼을 쏴 문을 열어보세요.");
    }

    public override void UpdateStep()
    {
        if(_door.IsOpened)
        {
            TutorialManager.Instance.NextTutorial();
        }
    }
}
