using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _attackCount;
    [SerializeField] private Image _iconContent;
    [SerializeField] private GameObject _overloadPanel;

    public void Init(WeaponProductData weaponProductData)
    {
        _iconContent.color = new Color(1, 1, 1, Convert.ToInt32(weaponProductData.weaponIconSprite != null));
        _iconContent.sprite = weaponProductData.weaponIconSprite;
    }
    
    public void SetAttackCount(int value)
    {
        _attackCount.text = value <= 0 ? "-" : value.ToString();
    }

    public void SetOverloadPanelActive(bool active)
    {
        _overloadPanel.SetActive(active);
    }
}
