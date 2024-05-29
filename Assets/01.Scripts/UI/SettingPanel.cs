using UnityEngine;

public class SettingPanel : MonoBehaviour
{
    private OptionElement[] _optionElements;

    private void Awake()
    {
        _optionElements = transform.GetComponentsInChildren<OptionElement>();
    }

    public void ApplySettings()
    {
        
    }
    
    public void ReturnSettings()
    {
        
    }
}
