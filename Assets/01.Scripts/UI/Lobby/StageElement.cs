using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StageElement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private TextMeshProUGUI _clearText;
    private Button _button;
    
    public void SetText(string text)
    {
        _text.text = text;
    }

    public void AddListener(UnityAction action)
    {
        _button ??= GetComponent<Button>();
        
        _button.onClick.AddListener(action);
    }

    public void SetMissionClear(bool active)
    {
        _clearText.gameObject.SetActive(active);
    }
}
