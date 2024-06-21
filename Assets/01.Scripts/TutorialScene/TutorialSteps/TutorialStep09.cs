using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialStep09 : TutorialStep
{
    public override void Enter()
    {
        TutorialManager.Instance.SetMessage("튜토리얼이 끝났습니다. 앞에 보이는 파란색 상자에 들어가\n실전으로 진입하십시오.");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            SceneManager.LoadScene("InGameScene");
        }
    }
}
