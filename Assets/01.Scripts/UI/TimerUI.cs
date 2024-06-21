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
        float ratio = Timer / StageManager.Instance.CurrentStage.clearTime;
        _timerText.color = Color.Lerp(Color.red, Color.white, ratio);

    }

    public void SetTime(float time)
    {
        _isTimerStart = false;
        Timer = time;
        _timerText.text = string.Format("{0:N2}", time);
        _timerText.color = Color.white;
    }

    public void StartTimer()
    {
        _isTimerStart = true;
    }
}
