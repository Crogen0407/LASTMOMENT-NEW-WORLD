using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    private Button _buyButton;
    private TextMeshProUGUI _buttonText;
    
    public void AddListener(UnityAction action)
    {
        if (_buyButton == null)
        {
            _buyButton = GetComponent<Button>();
        }
        _buyButton.onClick.AddListener(action);
    }

    public void ChangeState(WeaponOwnState weaponOwnState, int weaponPrice = 0)
    {
        if (_buyButton == null || _buttonText == null)
        {
            _buyButton = GetComponent<Button>();
            _buttonText = GetComponentInChildren<TextMeshProUGUI>();
        }
        
        switch (weaponOwnState)
        {
            case WeaponOwnState.Buy:
                _buyButton.image.color = Color.white;
                _buttonText.color = Color.white;
                _buttonText.text = $"구매 : {weaponPrice:000}";
                break;
            case WeaponOwnState.Used:
                _buyButton.image.color = Color.red;
                _buttonText.color = Color.red;
                _buttonText.text = "해제";
                break;
            case WeaponOwnState.Owned:
                _buyButton.image.color = Color.green;
                _buttonText.color = Color.green;
                _buttonText.text = "창착";
                break;
        }
    }
}
