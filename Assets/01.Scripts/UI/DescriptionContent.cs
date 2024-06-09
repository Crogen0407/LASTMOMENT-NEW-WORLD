using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionContent : MonoBehaviour
{
    [SerializeField] private Image _imageBox;
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _descriptionText;

    public void ShowDescription(DescriptionData descriptionData)
    {
        transform.localScale = Vector3.one;
        _imageBox.rectTransform.localScale = new Vector3(1, 0, 1);
        _imageBox.sprite = descriptionData.image;
        _imageBox.color = _imageBox.sprite == null ? Color.clear : Color.white;
        
        float duration = 0;
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(() => GameManager.Instance.InputReader.DisablePlayerActions());
        seq.Append(_imageBox.rectTransform.DOScaleY(1, 0.1f));
        seq.AppendCallback(() =>
        {
            duration = Typing(_titleText, descriptionData.title);
        });
        seq.AppendInterval(duration + 1);
        seq.AppendCallback(() => duration = 0);
        seq.AppendCallback(() =>
        {
            duration = Typing(_descriptionText, descriptionData.description);
        });
        seq.AppendInterval(duration + 1);
        seq.AppendCallback(() => GameManager.Instance.InputReader.EnablePlayerActions());
    }
    
    [ContextMenu("CloseDescription")]
    public void CloseDescription()
    {
        transform.DOScaleY(0, 1).SetEase(Ease.OutCubic);
    }

    #region Typing

    private bool isTyping = false;
    private float Typing(TextMeshProUGUI textMeshProUGUI, string str)
    {
        if (isTyping) return -1;
        StartCoroutine(TypingRoutine(textMeshProUGUI, str));
        return str.Length * 0.1f;
    }
    
    private IEnumerator TypingRoutine(TextMeshProUGUI textMeshProUGUI, string str)
    {
        textMeshProUGUI.text = string.Empty;
        isTyping = true;
        for (int i = 0; i < str.Length; ++i)
        {
            textMeshProUGUI.text += str[i];
            yield return new WaitForSeconds(0.1f);
        }
        isTyping = false;
    }

    #endregion
}
