using System;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class TalkContent : MonoSingleton<TalkContent>
{
    [SerializeField] private TextMeshProUGUI _nickNameText;
    [SerializeField] private TextMeshProUGUI _talkText;
    private CanvasGroup _canvasGroup;
    private bool _isTalking = false;
    
    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;
    }

    
    public async void OnTalk(string name, string talk)
    {
        if (_isTalking) return;
        await Task.Delay((int)(1000));
        _isTalking = true;
        _canvasGroup.alpha = 1;
        _nickNameText.text = name;
        _talkText.text = string.Empty;
        for (int i = 0; i < talk.Length; ++i)
        {
            await Task.Delay((int)(100));
            _talkText.text += talk[i];
        }
        _talkText.text = talk;
        await Task.Delay(1000);
        _canvasGroup.DOFade(0, 1).OnComplete(()=>_isTalking = false);
    }
    
    public async void OnTalk(string name, string talk, float textTypingSpeed = 0.1f, float lifeTime = 1, float startDelayTime = 1f, Action startEvent = null, Action endEvent = null)
    {
        if (_isTalking) return;
        await Task.Delay((int)(startDelayTime * 1000));
        startEvent?.Invoke();
        _isTalking = true;
        _canvasGroup.alpha = 1;
        _nickNameText.text = name;
        _talkText.text = string.Empty;
        for (int i = 0; i < talk.Length; ++i)
        {
            await Task.Delay((int)(textTypingSpeed * 1000));
            _talkText.text += talk[i];
        }
        _talkText.text = talk;
        await Task.Delay((int)(lifeTime * 1000));
        _canvasGroup.DOFade(0, lifeTime).OnComplete(()=>
        {
            _isTalking = false;
            endEvent?.Invoke();
        });
    }
    
    public async void OnTalk(string name, string talk, float startDelayTime = 1f, Action startEvent = null, Action endEvent = null)
    {
        if (_isTalking) return;
        await Task.Delay((int)(startDelayTime * 1000));
        startEvent?.Invoke();
        _isTalking = true;
        _canvasGroup.alpha = 1;
        _nickNameText.text = name;
        _talkText.text = string.Empty;
        for (int i = 0; i < talk.Length; ++i)
        {
            await Task.Delay(100);
            _talkText.text += talk[i];
        }
        _talkText.text = talk;
        await Task.Delay(1000);
        _canvasGroup.DOFade(0, 1).OnComplete(()=>
        {
            _isTalking = false;
            endEvent?.Invoke();
        });
    }
}