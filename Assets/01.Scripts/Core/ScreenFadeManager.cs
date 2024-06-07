using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFadeManager : MonoSingleton<ScreenFadeManager>
{
    private Image _image;
    private Transform _canvasTrm;
    
    private void InitFadeImage()
    {
        _canvasTrm = FindFirstObjectByType<Canvas>().transform;
        if (_image == null)
        {
            Image fadePanel = new GameObject().AddComponent<Image>();
            fadePanel.color = Color.black;

            _image = fadePanel;
        }
        _image.gameObject.SetActive(true);
        _image.transform.SetParent(_canvasTrm);
        _image.rectTransform.localPosition = Vector3.zero;
        _image.transform.localScale = Vector2.one * 100;
    }

    public void Fade(bool fadeType, float duration)
    {
        InitFadeImage();
        _image.gameObject.SetActive(true);
        Color startColor = new Color(0, 0, 0, Convert.ToInt32(fadeType));
        _image.color = startColor;

        _image.DOFade(1-startColor.a, duration).SetEase(Ease.InSine).SetUpdate(true).OnComplete(() =>
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

        _image.DOFade(1-startColor.a, duration).SetEase(Ease.InSine).SetUpdate(true).OnComplete(() =>
        {
            FadeObjectDestroy(_image, endEvent);
        });
    }

    private void FadeObjectDestroy(Image image, Action endEvent = null)
    {
        _image.transform.SetParent(null);
        if(_image.color.a <= 0.1f)
            _image.gameObject.SetActive(false);
        else
        {
            _image.gameObject.SetActive(true);
        }
        endEvent?.Invoke();
    }
}
