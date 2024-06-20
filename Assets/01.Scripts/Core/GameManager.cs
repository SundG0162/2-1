using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public float Timer { get; private set; }
    private bool _isGameStart = false;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (!_isGameStart) return;
        Timer += Time.deltaTime;
    }

    public void GameStart()
    {
        _isGameStart = true;
    }
}
