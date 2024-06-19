using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoSingleton<StageManager>
{
    [SerializeField]
    private List<Stage> _stageList;
    private Stage _currentStage; 
    public Stage CurrentStage => _currentStage;
    private int _currentStageIndex = 0;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        _currentStage = Instantiate(_stageList[0], transform);
    }

    public void NextStage()
    {
        Destroy(_currentStage.gameObject);
        _currentStageIndex++;
        PlayerManager.Instance.Player.MovementCompo.Teleport(_stageList[_currentStageIndex].startPosTrm.position);
        _currentStage = Instantiate(_stageList[_currentStageIndex], transform);
    }


    public void DeregisterEnemy(Enemy enemy)
    {
        _currentStage.enemyList.Remove(enemy);
    }

    public void Restart()
    {
        Destroy(_currentStage.gameObject);
        _currentStage = Instantiate(_stageList[_currentStageIndex], transform);
        PlayerManager.Instance.Player.MovementCompo.Teleport(_currentStage.startPosTrm.position);
    }
}
