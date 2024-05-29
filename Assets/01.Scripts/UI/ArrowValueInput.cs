using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class ArrowValueInput : MonoBehaviour
{
    //Components
    private Button _upButton;
    private Button _downButton;
    private TextMeshProUGUI _numberText;
    
    public UnityEvent onClickEvent;

    protected void Awake()
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
        onClickEvent?.Invoke();
    }

    protected virtual void HandleValueDown()
    {
        onClickEvent?.Invoke();
    }
    
    protected virtual void UpdateNumberText(string str)
    {
        _numberText.text = str;
    }
}
