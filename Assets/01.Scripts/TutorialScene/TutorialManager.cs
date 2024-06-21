using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TutorialManager : MonoSingleton<TutorialManager>
{
    [SerializeField]
    private TutorialPanelUI _tutorialPanelUI;
    [SerializeField]
    private List<TutorialStep> _tutorialStepList;
    private TutorialStep _currentStep;
    private int _currentStepIndex = 0;

    private void OnValidate()
    {
        _tutorialStepList = GetComponentsInChildren<TutorialStep>().ToList();
    }

    private void Awake()
    {
        _currentStep = _tutorialStepList[0];
        _currentStep.Enter();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        _currentStep.UpdateStep();
    }

    public void SetMessage(string msg)
    {
        _tutorialPanelUI.SetMessage(msg);
    }

    public void NextTutorial()
    {
        _currentStep.Exit();
        _currentStepIndex++;
        _currentStep = _tutorialStepList[_currentStepIndex];
        _currentStep.Enter();
    }

    public void Restart()
    {
        PlayerManager.Instance.Player.MovementCompo.Teleport(_tutorialStepList[_currentStepIndex-1].transform.position);
    }

    
}
