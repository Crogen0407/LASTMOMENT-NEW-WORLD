using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class ArrowValueInput<T> : MonoBehaviour
{
    //Components
    private Button _upButton;
    private Button _downButton;
    private TextMeshProUGUI _numberText;
    
    public UnityEvent<T> onClickEvent;
    
    [HideInInspector] public T value;

    protected virtual void Awake()
    {
        //Components
        _upButton = transform.Find("UpButton").GetComponent<Button>();
        _downButton = transform.Find("DownButton").GetComponent<Button>();
        _numberText = transform.Find("NumberText").GetComponent<TextMeshProUGUI>();
        
        _upButton.onClick.AddListener(HandleValueUp);
        _downButton.onClick.AddListener(HandleValueDown);
    }
    
    private void OnDestroy()
    {
        _upButton.onClick.RemoveListener(HandleValueUp);
        _downButton.onClick.RemoveListener(HandleValueDown);
    }

    protected virtual void HandleValueUp()
    {
        onClickEvent?.Invoke(value);
    }

    protected virtual void HandleValueDown()
    {
        onClickEvent?.Invoke(value);
    }
    
    protected virtual void UpdateNumberText(string str)
    {
        if (_numberText == null) return;
        _numberText.text = str;
    }

    public virtual void SetValue(T value)
    {
        this.value = value;
    }
}
