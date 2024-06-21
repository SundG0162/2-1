using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndPanelUI : MonoBehaviour
{
    [SerializeField]
    private Button _titleBtn, _quitBtn;
    [SerializeField]
    private TextMeshProUGUI _retryCountText;

    private void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        _titleBtn.onClick.AddListener(() => SceneManager.LoadScene("TitleScene"));
        _quitBtn.onClick.AddListener(() => Application.Quit());
        _retryCountText.text = $"����� ��õ� Ƚ���� ������ �����ϴ�.\n:{PlayerPrefs.GetInt("RetryCount")}";
    }
}
