using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoSingleton<StageManager>
{
    [SerializeField]
    private List<Stage> _stageList;
    private Stage _currentStage;
    public Stage CurrentStage => _currentStage;
    private int _currentStageIndex = 0;
    private int _retryCount = 0;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        _currentStage = Instantiate(_stageList[0], transform);
        GameManager.Instance.SetTimer(_currentStage.clearTime);
    }

    public void NextStage()
    {
        Destroy(_currentStage.gameObject);
        _currentStageIndex++;
        if(_currentStageIndex >= _stageList.Count) 
        {
            PlayerPrefs.SetInt("RetryCount", _retryCount);
            SceneManager.LoadScene("EndScene");
            return;
        }
        _currentStage = Instantiate(_stageList[_currentStageIndex], transform);
        PlayerManager.Instance.Player.MovementCompo.Teleport(_currentStage.startPosTrm.position);
        GameManager.Instance.SetTimer(_currentStage.clearTime);
    }


    public void DeregisterEnemy(Enemy enemy)
    {
        _currentStage.enemyList.Remove(enemy);
    }

    public void Restart()
    {
        _retryCount++;
        Destroy(_currentStage.gameObject);
        _currentStage = Instantiate(_stageList[_currentStageIndex], transform);
        PlayerManager.Instance.Player.MovementCompo.Teleport(_currentStage.startPosTrm.position);
        GameManager.Instance.SetTimer(_currentStage.clearTime);
    }
}
