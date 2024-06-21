using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField]
    private TimerUI _timerUI;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if(_timerUI.IsTimerStart && _timerUI.Timer <= 0)
        {
            StageManager.Instance.Restart();
        }
    }

    public void StartTimer()
    {
        _timerUI.StartTimer();
    }

    public void SetTimer(float Time)
    {
        _timerUI.SetTime(Time);
    }
}
