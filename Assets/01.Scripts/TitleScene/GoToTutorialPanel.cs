using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoToTutorialPanel : MonoBehaviour
{
    [SerializeField]
    private Button _yesBtn, _noBtn;

    private void Awake()
    {
        _yesBtn.onClick.AddListener(() => SceneManager.LoadScene("TutorialScene"));
        _noBtn.onClick.AddListener(() => SceneManager.LoadScene("InGameScene"));
    }

}
