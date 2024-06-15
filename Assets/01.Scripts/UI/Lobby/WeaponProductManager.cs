using TMPro;
using UnityEngine;

public class WeaponProductManager : MonoSingleton<WeaponProductManager>
{
    [field:SerializeField] public WeaponProductDataSO WeaponProductData { get; private set; }
    
    [SerializeField] private RectTransform _weaponProductContent;
    [SerializeField] private CurrentWeaponMenuContent _currentWeaponMenuContent;
    [SerializeField] private TextMeshProUGUI _commodityText;
    
    
    
}