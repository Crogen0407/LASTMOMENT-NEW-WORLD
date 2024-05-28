using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ArrowListInput : MonoBehaviour
{
    //Components
    private Button _upButton;
    private Button _downButton;
    private TextMeshProUGUI _numberText;
    
    public List<string> list;
    public int currentSelectedIndex;
    
    public UnityEvent onClickEvent;
    
    private void Awake()
    {
        //Components
        _upButton = transform.Find("UpButton").GetComponent<Button>();
        _downButton = transform.Find("DownButton").GetComponent<Button>();
        _numberText = transform.Find("NumberText").GetComponent<TextMeshProUGUI>();
        UpdateNumberText();
        _upButton.onClick.AddListener(HandleValueUp);
        _downButton.onClick.AddListener(HandleValueDown);
    }
    
    private void OnDestroy()
    {
        _upButton.onClick.RemoveListener(HandleValueUp);
        _downButton.onClick.RemoveListener(HandleValueDown);
    }

    private void HandleValueUp()
    {
        onClickEvent?.Invoke();
        currentSelectedIndex = (currentSelectedIndex+1) % list.Count;
        UpdateNumberText();
    }

    private void HandleValueDown()
    {
        onClickEvent?.Invoke();
        currentSelectedIndex = (currentSelectedIndex-1) % list.Count;
        UpdateNumberText();
    }

    private void UpdateNumberText()
    {
        _numberText.text = list[currentSelectedIndex];
    }
}
