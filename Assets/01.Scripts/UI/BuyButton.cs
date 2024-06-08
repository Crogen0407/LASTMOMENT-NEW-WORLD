using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public enum WeaponOwnState
{
    Buy,
    Used,
    Owned
}

public class BuyButton : MonoBehaviour
{
    [SerializeField] private WeaponOwnState _weaponOwnState;
    [SerializeField] private WeaponEnum _weaponEnum;
    private WeaponProductData _weaponProductData;
    
    //Components
    private TextMeshProUGUI _weaponNameText;
    private ProductContent _productContent;
    private Button _buyButton;
    private TextMeshProUGUI _buttonText;
    
    private void ResetValue()
    {
        _weaponNameText = transform.Find("WeaponNameText").GetComponent<TextMeshProUGUI>();
        _productContent = GetComponentInParent<ProductContent>();
        _buyButton = transform.Find("BuyButton").GetComponent<Button>();
        _buttonText = _buyButton.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        
        _weaponProductData = _productContent.productData.weaponProductDataDictionary[_weaponEnum];
            
        _weaponNameText.text = _weaponProductData.weaponName;
    }

    public void Init(WeaponOwnState weaponOwnState)
    {
        if (_weaponNameText != null)
        {
            ResetValue();
            _buyButton.onClick.AddListener(HandleUpdateWeaponOwnState);
        }
        
        _weaponOwnState = weaponOwnState;
        UpdateButtonText(_weaponOwnState);
    }

    private void HandleUpdateWeaponOwnState()
    {
        switch (_weaponOwnState)
        {
            case WeaponOwnState.Buy:
                //구매
                CommodityManager.Instance.AddGoldAndPreamble(-_weaponProductData.weaponPrice, 0);
                _weaponOwnState = WeaponOwnState.Owned;
                break;
            case WeaponOwnState.Used:
                //해제
                _weaponOwnState = WeaponOwnState.Owned;
                
                break;
            case WeaponOwnState.Owned:
                //장착
                _weaponOwnState = WeaponOwnState.Used;
                break;
        }
        UpdateButtonText(_weaponOwnState);
        _productContent.ApplyWeaponOwnData(_weaponEnum, _weaponOwnState, _weaponProductData.weaponIconSprite, _weaponProductData.weaponName);
    }

    private void UpdateButtonText(WeaponOwnState weaponOwnState)
    {
        switch (weaponOwnState)
        {
            case WeaponOwnState.Buy:
                _buttonText.text = $"구매 : {_weaponProductData.weaponPrice:000}";
                break;
            case WeaponOwnState.Used:
                _buttonText.text = "해제";
                break;
            case WeaponOwnState.Owned:
                _buttonText.text = "창착";
                break;
        }
    }
}
