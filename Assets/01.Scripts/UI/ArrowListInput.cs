using System.Collections.Generic;

public class ArrowListInput : ArrowValueInput
{
    public List<string> list;
    public int currentSelectedIndex;
    
    private void Awake()
    {
        base.Awake();
        UpdateNumberText(list[currentSelectedIndex]);
    }

    protected override void HandleValueUp()
    {
        base.HandleValueUp();
        onClickEvent?.Invoke();
        currentSelectedIndex = (currentSelectedIndex+1) % list.Count;
        UpdateNumberText(list[currentSelectedIndex]);
    }

    protected override void HandleValueDown()
    {
        base.HandleValueDown();
        onClickEvent?.Invoke();
        currentSelectedIndex = (currentSelectedIndex-1) % list.Count;
        UpdateNumberText(list[currentSelectedIndex]);
    }
}
