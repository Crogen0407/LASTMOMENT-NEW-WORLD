using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageContent : MonoSingleton<MessageContent>
{
    private Image _outlineImage;
    private TextMeshProUGUI _text;
    private CanvasGroup _canvasGroup;
    
    private void Awake()
    {
        transform.localScale = new Vector3(1, 0);
        _canvasGroup = GetComponent<CanvasGroup>();
        _outlineImage = transform.Find("Outline").GetComponent<Image>();
        _text = GetComponentInChildren<TextMeshProUGUI>();

        _canvasGroup.alpha = 0;
    }

    private bool _isShowMessage = false;
    public void ShowMessage(string message, Color color)
    {
        if (_isShowMessage) return;
        _isShowMessage = true;
        _outlineImage.color = color;
        _text.color = color;
        _text.text = message;
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScaleY(1, 0.2f))
            .Join(_canvasGroup.DOFade(1, 0.1f));
        
        seq.AppendInterval(2);

        seq.Append(transform.DOScaleY(0, 0.2f));
        seq.Append(_canvasGroup.DOFade(0, 0.1f)).OnComplete(() =>
        {
            _isShowMessage = false;
        });
    }
}
