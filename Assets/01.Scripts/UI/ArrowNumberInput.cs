using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ArrowNumberInput : ArrowValueInput
{
    [SerializeField] private int _maxValue = 10; 
    [SerializeField] private int _minValue = 0;
    [SerializeField] private int _defaultValue = 5;
    [HideInInspector] public int value;

    private void Awake()
    {
        base.Awake();
        value = _defaultValue;
        UpdateNumberText(value.ToString());
    }

    private void Reset()
    {
        _defaultValue = (_maxValue + _minValue) / 2;
        value = _defaultValue;
        transform.Find("NumberText").GetComponent<TextMeshProUGUI>().text = value.ToString();
    }

    protected override void HandleValueUp()
    {
        base.HandleValueUp();
        if (value == _maxValue) return;
        ++value;
        UpdateNumberText(value.ToString());
    }

    protected override void HandleValueDown()
    {
        base.HandleValueDown();
        if (value == _minValue) return;
        --value;
        UpdateNumberText(value.ToString());
    }
}