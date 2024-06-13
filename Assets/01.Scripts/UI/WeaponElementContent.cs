using TMPro;
using UnityEngine;

public enum WeaponOwnState
{
    Buy,
    Used,
    Owned
}

public class WeaponElementContent : MonoBehaviour
{
    [SerializeField] private WeaponOwnState _weaponOwnState;
    [SerializeField] private WeaponEnum _weaponEnum;
    private WeaponProductData _weaponProductData;
    
    //Components
    private TextMeshProUGUI _weaponNameText;
    private ProductContent _productContent;
    private BuyButton _buyButton;
    
    public void Init(WeaponOwnState weaponOwnState)
    {
        if (_weaponNameText == null)
        {
            _weaponNameText = transform.Find("WeaponNameText").GetComponent<TextMeshProUGUI>();
            _productContent = GetComponentInParent<ProductContent>();
            _buyButton = GetComponentInChildren<BuyButton>();
            
            _weaponProductData = _productContent.productData.weaponProductDataDictionary[_weaponEnum];
            _weaponNameText.text = _weaponProductData.weaponName;
            
            _buyButton.AddListener(HandleUpdateWeaponOwnState);
        }
        
        _weaponOwnState = weaponOwnState;
        UpdateButtonText(_weaponOwnState);
    }

    //버튼의 상태를 전환
    private void HandleUpdateWeaponOwnState()
    {
        switch (_weaponOwnState)
        {
            case WeaponOwnState.Buy:
                //구매
                if (GameDataManager.Instance.Gold >= _weaponProductData.weaponPrice)
                {
                    GameDataManager.Instance.AddGold(-_weaponProductData.weaponPrice);
                    _productContent.UpdateCommodity();
                    _weaponOwnState = WeaponOwnState.Owned;
                }
                break;
            case WeaponOwnState.Used:
                //해제
                _weaponOwnState = WeaponOwnState.Owned;
                _productContent.ApplyWeaponOwnData(_weaponEnum, _weaponOwnState, _weaponProductData.weaponIconSprite, _weaponProductData.weaponName);
                break;
            case WeaponOwnState.Owned:
                //장착
                _weaponOwnState = WeaponOwnState.Used;
                _productContent.ApplyWeaponOwnData(_weaponEnum, _weaponOwnState, _weaponProductData.weaponIconSprite, _weaponProductData.weaponName);
                break;
        }
        UpdateButtonText(_weaponOwnState);
    }

    private void UpdateButtonText(WeaponOwnState weaponOwnState)
    {
        _buyButton.ChangeState(weaponOwnState, _weaponProductData.weaponPrice);
    }
}
