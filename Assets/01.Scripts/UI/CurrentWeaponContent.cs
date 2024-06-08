using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrentWeaponContent : MonoBehaviour
{
    private Image _iconImage;
    private TextMeshProUGUI _nameText;

    private void Awake()
    {
        _iconImage = transform.Find("Icon").GetComponent<Image>();
        _nameText = transform.Find("Text").GetComponent<TextMeshProUGUI>();
    }

    public void ApplyCurrentWeaponData(WeaponProductData weaponProductData)
    {
        _iconImage.sprite = weaponProductData.weaponIconSprite;
        if (weaponProductData.weaponIconSprite != null)
            _iconImage.color = Color.white;
        else
            _iconImage.color = Color.clear;
        _nameText.text = weaponProductData.weaponName;
    }
}
