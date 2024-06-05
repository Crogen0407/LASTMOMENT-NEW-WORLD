using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFade : MonoSingleton<ScreenFade>
{
    private Image _image;
    
    private void InitFadeImage()
    {
        if (_image == null)
        {
            Image fadePanel = new GameObject().AddComponent<Image>();
            fadePanel.color = Color.black;

            Transform rectTrm = fadePanel.transform;

            rectTrm.localScale = Vector2.one * 100;
            _image = Instantiate(fadePanel, UIManager.Instance.gameCanvas.transform);
        }
    }
    
    public void Fade(bool fadeType, float duration)
    {
        InitFadeImage();
        _image.gameObject.SetActive(true);
        Color startColor = new Color(0, 0, 0, Convert.ToInt32(fadeType));
        _image.color = startColor;

        _image.DOColor(new Color(0, 0, 0, Convert.ToInt32(!fadeType)), duration).SetEase(Ease.InSine).OnComplete(() =>
        {
            FadeObjectDestroy(_image);
        });
    }
        
    public void Fade(bool fadeType, float duration, Action endEvent)
    { 
        InitFadeImage();
        _image.gameObject.SetActive(true);
        Color startColor = new Color(0, 0, 0, Convert.ToInt32(fadeType));
        _image.color = startColor;

        _image.DOColor(new Color(0, 0, 0, Convert.ToInt32(!fadeType)), duration).SetEase(Ease.InSine).OnComplete(() =>
        {
            FadeObjectDestroy(_image, endEvent);
        });
    }

    private void FadeObjectDestroy(Image image, Action endEvent = null)
    {
        if (_image.color.a == 0)
        {
            _image.gameObject.SetActive(false);
        }
        endEvent?.Invoke();
    }
}
