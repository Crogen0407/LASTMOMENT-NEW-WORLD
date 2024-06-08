using System;
using Crogen.JsamJson;
using TMPro;
using UnityEngine;

public class ProductContent : MonoBehaviour
{
    private GameData _gameData;
    public int[] weaponOwnStateArray;
    public int[] currentWeaponArray;
    [SerializeField] private CurrentWeaponContent[] _curWeaponContents;
    [SerializeField] private BuyButton[] _buyButtons;
    public ProductDataSO productData;

    [Header("Commodity")] 
    [SerializeField] private TextMeshProUGUI _commodityText;
    
    private void OnEnable()
    {
        _commodityText.text = $"골드 : {CommodityManager.Instance.GetGold().ToString()} / 전언 : {CommodityManager.Instance.GetPreamble().ToString()}";
        
        LoadData();
        for (int i = 0; i < _buyButtons.Length; ++i)
        {
            _buyButtons[i].Init((WeaponOwnState)weaponOwnStateArray[i]);
        }
        for (int i = 0; i < _curWeaponContents.Length; ++i)
        {
            WeaponData weaponData = productData.weaponDataDictionary[(WeaponEnum)currentWeaponArray[i]];
            _curWeaponContents[i].ApplyCurrentWeaponData(weaponData);
        }
    }

    public void ApplyWeaponOwnData(WeaponEnum weapon, WeaponOwnState weaponOwnState, Sprite sprite, string weaponName)
    {
        weaponOwnStateArray[(int)weapon-1] = (int)weaponOwnState;
        for (int i = 0; i < _curWeaponContents.Length; ++i)
        {
            if (currentWeaponArray[i] == 0)
            {
                if (weaponOwnState == WeaponOwnState.Used)
                {
                    _curWeaponContents[i].ApplyCurrentWeaponData(new WeaponData
                    {
                        weaponIconSprite = null, 
                        weaponName = "-"
                    });
                    currentWeaponArray[i] = (int)weapon-1;
                }
                else if(weaponOwnState == WeaponOwnState.Owned)
                {
                    _curWeaponContents[i].ApplyCurrentWeaponData(new WeaponData
                    {
                        weaponIconSprite = sprite,
                        weaponName = weaponName
                    });
                    currentWeaponArray[i] = (int)weapon-1;
                }
                break;
            }
        }
    }

    private void OnDisable()
    {
        SaveData();
    }

    private void LoadData()
    {
        _gameData = JsamJson.Load<GameData>(false);
        weaponOwnStateArray = _gameData.weaponOwnStateArray;
        currentWeaponArray = _gameData.currentWeaponArray;

        int weaponEnumLength = Enum.GetNames(typeof(WeaponEnum)).Length - 1;
        int weaponOwnStateLength = Enum.GetNames(typeof(WeaponOwnState)).Length;
        
        if (currentWeaponArray == null || currentWeaponArray.Length != weaponOwnStateLength)
            currentWeaponArray = new int[weaponOwnStateLength];
        
        if (weaponOwnStateArray == null || weaponOwnStateArray.Length != weaponEnumLength)
            weaponOwnStateArray = new int[weaponEnumLength];
    }

    private void SaveData()
    {
        GameData gameData = new GameData()
        {
            gold = _gameData.gold,
            preamble = _gameData.preamble,
            settingArray = _gameData.settingArray,
            weaponOwnStateArray = weaponOwnStateArray,
            currentWeaponArray = currentWeaponArray
        };
        JsamJson.Save<GameData>(gameData, false);
    }
}
