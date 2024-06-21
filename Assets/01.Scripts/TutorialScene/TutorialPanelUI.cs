using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialPanelUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _tutorialText;

    public void SetMessage(string msg)
    {
        _tutorialText.text = msg;
    }
}
