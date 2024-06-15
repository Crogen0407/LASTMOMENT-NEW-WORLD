using UnityEngine;

public enum WeaponOwnState
{
    Buy,
    Used,
    Owned
}

public class WeaponElementContent : MonoBehaviour
{
    private BuyButton _buyButton;

    public void Init(WeaponEnum weaponEnum, WeaponOwnState weaponOwnState)
    {
        if(_buyButton == null)
            _buyButton = GetComponentInChildren<BuyButton>();

        var weaponProductData = WeaponProductManager.Instance.WeaponProductData.weaponProductDataDictionary[weaponEnum];
        _buyButton.SetWeaponData(weaponOwnState, weaponProductData.weaponPrice);
    }
}
