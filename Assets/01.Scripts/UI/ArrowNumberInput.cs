using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ArrowNumberInput : MonoBehaviour
{
    //Components
    private Button _upButton;
    private Button _downButton;
    private TextMeshProUGUI _numberText;

    [SerializeField] private int _maxValue = 10; 
    [SerializeField] private int _minValue = 0; 
    [HideInInspector] public int value;

    public UnityEvent onClickEvent;
    
    private void Awake()
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

    private void HandleValueUp()
    {
        onClickEvent?.Invoke();
        if (value == _maxValue) return;
        ++value;
        UpdateNumberText(value);
    }

    private void HandleValueDown()
    {
        onClickEvent?.Invoke();
        if (value == _minValue) return;
        --value;
        UpdateNumberText(value);
    }

    private void UpdateNumberText(int value)
    {
        _numberText.text = value.ToString();
    }
}