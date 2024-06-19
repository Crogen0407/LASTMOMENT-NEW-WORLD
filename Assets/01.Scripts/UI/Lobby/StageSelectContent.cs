using DG.Tweening;
using UnityEngine;

public class StageSelectContent : MonoBehaviour
{
    private RectTransform _rectTransform;

    [SerializeField] private float _minPos;
    [SerializeField] private float _maxPos;
    private bool _isActive;
    private void Awake()
    {
        _rectTransform = transform as RectTransform;
    }

    public void SetActiveStageSelectContent()
    {
        float endPos = 0f;
        _isActive = !_isActive;
        endPos = _isActive ? _minPos : _maxPos;
        _rectTransform.DOAnchorPosX(endPos, 0.5f);
    }
}
