using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialStep09 : TutorialStep
{
    public override void Enter()
    {
        TutorialManager.Instance.SetMessage("Ʃ�丮���� �������ϴ�. �տ� ���̴� �Ķ��� ���ڿ� ��\n�������� �����Ͻʽÿ�.");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            SceneManager.LoadScene("InGameScene");
        }
    }
}
