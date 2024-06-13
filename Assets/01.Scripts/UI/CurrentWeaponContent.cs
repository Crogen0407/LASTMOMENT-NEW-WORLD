using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CurrentWeaponContent : MonoBehaviour
{
    private Image _iconImage;
    private TextMeshProUGUI _nameText;
    private Button _deleteBtn;
    
    public WeaponEnum weaponEnum;
    public void ApplyCurrentWeaponData(WeaponEnum weaponEnum, WeaponProductData weaponProductData)
    {
        if (_iconImage == null)
        {
            _iconImage = transform.Find("Icon").GetComponent<Image>();
            _nameText = transform.Find("Text").GetComponent<TextMeshProUGUI>();
            _deleteBtn = transform.Find("DeleteButton").GetComponent<Button>();
            _deleteBtn.onClick.AddListener(HandleOnDelete);
        }
        
        if (weaponEnum == WeaponEnum.None)
        {
            _iconImage.color = Color.clear;    
            _nameText.text = weaponProductData.weaponName;
            return;
        }
        this.weaponEnum = weaponEnum;
        
        if (weaponProductData.weaponIconSprite != null)
        {
            _iconImage.sprite = weaponProductData.weaponIconSprite;
            _iconImage.color = Color.white;
        }
    }

    private void HandleOnDelete()
    {
        ApplyCurrentWeaponData(WeaponEnum.None, new WeaponProductData());
    }
}
