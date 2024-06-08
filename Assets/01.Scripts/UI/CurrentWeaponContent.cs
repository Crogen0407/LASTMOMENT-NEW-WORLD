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

    public void ApplyCurrentWeaponData(WeaponData weaponData)
    {
        _iconImage.sprite = weaponData.weaponIconSprite;
        if (weaponData.weaponIconSprite != null)
            _iconImage.color = Color.white;
        else
            _iconImage.color = Color.clear;
        _nameText.text = weaponData.weaponName;
    }
}
