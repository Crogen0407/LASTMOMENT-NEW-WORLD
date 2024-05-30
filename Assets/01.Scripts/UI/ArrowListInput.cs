using System.Collections.Generic;

public class ArrowListInput : ArrowValueInput<int>
{
    public List<string> list;
    
    protected override void Awake()
    {
        base.Awake();
        UpdateNumberText(list[value]);
    }

    protected override void HandleValueUp()
    {
        ++value;
        if (value > list.Count-1) value = 0;
        UpdateNumberText(list[value]);
        base.HandleValueUp();
    }

    protected override void HandleValueDown()
    {
        --value;
        if (value < 0) value = list.Count - 1;
        UpdateNumberText(list[value]);
        base.HandleValueDown();
    }
    
    public override void SetValue(int value)
    {
        base.SetValue(value);
        UpdateNumberText(list[value]);
    }
}
