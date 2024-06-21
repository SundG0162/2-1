using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TitlePanelUI : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _canvasGroup;
    [SerializeField]
    private TextMeshProUGUI _titleText;
    private Tween _titleTextBlinkTween;

    private void Start()
    {
        _titleTextBlinkTween =
            _titleText.DOFade(0.4f, 1.2f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }


    public void DisableUI()
    {
        _titleTextBlinkTween.Kill();
        _canvasGroup.DOFade(0, 0.5f);
    }
}
