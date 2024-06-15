using Crogen.PowerfulInput;
using TMPro;
using UnityEngine;

public class WeaponProductManager : MonoSingleton<WeaponProductManager>
{
    [field:SerializeField] public WeaponProductDataSO WeaponProductData { get; private set; }

    [SerializeField] private Canvas _shopCanvas;
    [SerializeField] private RectTransform _weaponProductContent;
    public CurrentWeaponMenuContent currentWeaponMenuContent;
    public WeaponProductContent weaponProductContent;
    [SerializeField] private TextMeshProUGUI _commodityText;

    [field: SerializeField] private InputReader InputReader;
    
    public bool IsFullCurrentWeaponContainer { get; set; } = false;

    private void Awake()
    {
        DisableShopCanvas();
        InputReader.EscEvent += DisableShopCanvas;
    }

    private void OnDestroy()
    {
        InputReader.EscEvent -= DisableShopCanvas;
    }

    private void DisableShopCanvas()
    {
        _shopCanvas.gameObject.SetActive(false);
    }
}