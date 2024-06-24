using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrentWeaponContent : MonoBehaviour
{
    private WeaponProductDataSO _weaponProductData;
    [SerializeField] private Image _iconImage;
    [SerializeField] private TextMeshProUGUI _nameText;

    private WeaponEnum _weaponEnum;

    public WeaponEnum WeaponEnum    
    {
        get => _weaponEnum;
        set
        {
            if (_weaponProductData == null)
                _weaponProductData = WeaponProductManager.Instance.WeaponProductData;
            _weaponEnum = value;

            //Icon Image
            _iconImage.sprite = _weaponProductData.weaponProductDataDictionary[_weaponEnum].weaponIconSprite;
            _iconImage.color = _iconImage.sprite == null ? Color.clear : Color.white;

            //Name Text
            _nameText.text = _weaponProductData.weaponProductDataDictionary[_weaponEnum].weaponName;
        }
    }
}