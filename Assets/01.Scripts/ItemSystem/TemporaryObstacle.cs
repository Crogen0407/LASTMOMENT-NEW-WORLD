using DG.Tweening;
using UnityEngine;

public class TemporaryObstacle : MonoPoolingObject
{
    //Components
    private Collider _collider;
    
    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }
    
    public override void OnPop()
    {
        transform.localScale = Vector3.zero;
        _collider.enabled = false;
        transform.DOScale(Vector3.one * 8, 0.7f).SetEase(Ease.InOutElastic).OnComplete(() =>
        {
            _collider.enabled = true;
            TalkContent.Instance.OnTalk("System", "방해물 오브젝트 설치 완료");
        });
    }
    
    public override void OnPush()
    {
    }
}
