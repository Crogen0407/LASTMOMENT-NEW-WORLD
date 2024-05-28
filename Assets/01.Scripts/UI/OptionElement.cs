using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OptionElement : MonoBehaviour
{
    private Button _button;
    public UnityEvent onClickEvent;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(HandleClick);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(HandleClick);
    }

    private void HandleClick()
    {
        onClickEvent?.Invoke();
    }
}
