using System;
using TMPro;
using UnityEngine;

public class ProductContent : MonoBehaviour
{
    //Managements
    private GameDataManager _gameDataManager;
    
    public int[] currentWeaponArray;
    public int[] weaponOwnStateArray;
    [SerializeField] private CurrentWeaponContent[] _curWeaponContents;
    [SerializeField] private WeaponElementContent[] _buyButtons;
    public ProductDataSO productData;

    [Header("Commodity")] 
    [SerializeField] private TextMeshProUGUI _commodityText;
    
    private void OnEnable()
    {
        _gameDataManager = GameDataManager.Instance;
        UpdateCommodity();        
        LoadData();
        for (int i = 0; i < _buyButtons.Length; ++i)
        {
            _buyButtons[i].Init((WeaponOwnState)weaponOwnStateArray[i]);
        }
        for (int i = 0; i < _curWeaponContents.Length; ++i)
        {
            WeaponProductData weaponProductData = productData.weaponProductDataDictionary[(WeaponEnum)currentWeaponArray[i]];
            _curWeaponContents[i].ApplyCurrentWeaponData((WeaponEnum)currentWeaponArray[i], weaponProductData);
        }
    }

    public void UpdateCommodity()
    {
        _commodityText.text = $"골드 : {GameDataManager.Instance.Gold:C0}";
        _gameDataManager.SaveData();
    }

    //현재 장착하고 있는 무기의 데이터를 변경
    public void ApplyWeaponOwnData(WeaponEnum weapon, WeaponOwnState weaponOwnState, Sprite sprite, string weaponName)
    {
        if (weaponOwnState == WeaponOwnState.Used)
        {
            for (int i = 0; i < _curWeaponContents.Length; ++i)
            {
                if (currentWeaponArray[i] == 0)
                {
                    _curWeaponContents[i].ApplyCurrentWeaponData(weapon,
                        new WeaponProductData
                        {
                            weaponIconSprite = sprite,
                            weaponName = weaponName
                        });
                    currentWeaponArray[i] = (int)weapon-1;
                    break;
                }
                MessageContent.Instance.ShowMessage("장착할 수 있는 공간이 부족합니다", Color.white);
            }
        }
        else if(weaponOwnState == WeaponOwnState.Owned)
        {
            for (int i = 0; i < _curWeaponContents.Length; ++i)
            {
                if (_curWeaponContents[i].weaponEnum == weapon)
                {
                    _curWeaponContents[i].ApplyCurrentWeaponData(weapon, productData.weaponProductDataDictionary[WeaponEnum.None]);
                    currentWeaponArray[i] = (int)weapon-1;
                    break;
                }
            }
        }
        weaponOwnStateArray[(int)weapon-1] = (int)weaponOwnState;
    }

    private void OnDisable()
    {
        _gameDataManager.GameData.currentWeaponArray = currentWeaponArray;
        _gameDataManager.GameData.weaponOwnStateArray = weaponOwnStateArray;
        _gameDataManager.SaveData();
    }

    private void LoadData()
    {
        //데이터 불러오고
        _gameDataManager.LoadData();
        
        currentWeaponArray = _gameDataManager.GameData.currentWeaponArray;
        weaponOwnStateArray = _gameDataManager.GameData.weaponOwnStateArray;

        //데이터가 제대로 불러와졌는지 확인
        int weaponEnumLength = Enum.GetNames(typeof(WeaponEnum)).Length - 1;
        int weaponOwnStateLength = Enum.GetNames(typeof(WeaponOwnState)).Length;
        
        if (currentWeaponArray.Length == 0 || currentWeaponArray.Length != weaponOwnStateLength)
            currentWeaponArray = new int[weaponOwnStateLength];
        
        if (weaponOwnStateArray.Length == 0 || weaponOwnStateArray.Length != weaponEnumLength)
            weaponOwnStateArray = new int[weaponEnumLength];

        //UI에다가 불러온 정보 적용
        for (int i = 0; i < _curWeaponContents.Length; ++i)
        {
            _curWeaponContents[i].ApplyCurrentWeaponData((WeaponEnum)currentWeaponArray[i], productData.weaponProductDataDictionary[(WeaponEnum)currentWeaponArray[i]]);
        }
        for (int i = 0; i < _buyButtons.Length; ++i)
        {
            _buyButtons[i].Init((WeaponOwnState)weaponOwnStateArray[i]);
        }
    }
}
