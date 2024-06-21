using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    public float Timer { get; private set; }
    private bool _isTimerStart = false;
    public bool IsTimerStart => _isTimerStart;

    [SerializeField]
    private TextMeshProUGUI _timerText;

    private void Update()
    {
        if (!_isTimerStart) return;
        Timer -= Time.deltaTime;
        _timerText.text = string.Format("{0:N2}", Timer);

    }

    public void SetTime(float time)
    {
        _isTimerStart = false;
        Timer = time;
        Debug.Log(time);
        _timerText.text = string.Format("{0:N2}", time);
    }

    public void StartTimer()
    {
        _isTimerStart = true;
    }
}
