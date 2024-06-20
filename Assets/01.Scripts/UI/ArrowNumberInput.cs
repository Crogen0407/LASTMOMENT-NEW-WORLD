using TMPro;
using UnityEngine;

public class ArrowNumberInput : ArrowValueInput<int>
{
    [SerializeField] private int _maxValue = 10; 
    [SerializeField] private int _minValue = 0;
    [SerializeField] private int _defaultValue = 5;

    private void Awake()
    {
        base.Awake();
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
        if (value == _maxValue) return;
        ++value;
        UpdateNumberText(value.ToString());
        base.HandleValueUp();
    }

    protected override void HandleValueDown()
    {
        if (value == _minValue) return;
        --value;
        UpdateNumberText(value.ToString());
        base.HandleValueDown();
    }

    public override void SetValue(int value)
    {
        base.SetValue(value);
        UpdateNumberText(value.ToString());
    }
}