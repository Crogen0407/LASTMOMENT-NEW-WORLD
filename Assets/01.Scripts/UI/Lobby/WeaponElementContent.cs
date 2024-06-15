using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public enum WeaponOwnState
{
    Buy,
    Used,
    Owned
}

public class WeaponElementContent : MonoBehaviour
{
    //Managements
    private GameDataManager _gameDataManager;
    
    public WeaponProductData weaponProductData;
    public WeaponEnum weaponEnum;
    public WeaponOwnState weaponOwnState;
    public BuyButton buyButton;

    public void Init(WeaponEnum weaponEnum, WeaponOwnState weaponOwnState)
    {
        if(_gameDataManager==null)
            _gameDataManager = GameDataManager.Instance;
        
        this.weaponEnum = weaponEnum;
        this.weaponOwnState = weaponOwnState;
        
        if(buyButton == null)
            buyButton = GetComponentInChildren<BuyButton>();

        weaponProductData = WeaponProductManager.Instance.WeaponProductData.weaponProductDataDictionary[weaponEnum];
        buyButton.SetWeaponData(weaponOwnState, weaponProductData.weaponPrice);
        buyButton.AddListener(HandleOnBuyButtonClick);
    }

    private void HandleOnBuyButtonClick()
    {
        switch (weaponOwnState)
        {
            //구매했을 때
            case WeaponOwnState.Buy:
                _gameDataManager.AddGold(-weaponProductData.weaponPrice);
                weaponOwnState = WeaponOwnState.Owned;
                _gameDataManager.GameData.weaponOwnStateArray[(int)weaponEnum] = (int)WeaponOwnState.Owned; 
                buyButton.SetWeaponData(WeaponOwnState.Owned);
                GameDataManager.Instance.SaveData();
                break;
            case WeaponOwnState.Used:
                if (_gameDataManager.CurrentWeaponArray.Any(x => x == (int)weaponEnum))
                {
                    for (int i = 0; i < _gameDataManager.CurrentWeaponArray.Length; ++i)
                    {
                        if (_gameDataManager.CurrentWeaponArray[i] == (int)weaponEnum)
                        {
                            _gameDataManager.CurrentWeaponArray[i] = (int)WeaponEnum.None;
                            WeaponProductManager.Instance.currentWeaponMenuContent.SetCurrentWeaponContent(i, WeaponEnum.None);
                            WeaponProductManager.Instance.IsFullCurrentWeaponContainer = false;
                            break;
                        }
                    }
                    _gameDataManager.GameData.weaponOwnStateArray[(int)weaponEnum] = (int)WeaponOwnState.Owned;
                    weaponOwnState = WeaponOwnState.Owned;
                    buyButton.SetWeaponData(WeaponOwnState.Owned);
                    GameDataManager.Instance.SaveData();
                }
                break;
            case WeaponOwnState.Owned:
                if (WeaponProductManager.Instance.IsFullCurrentWeaponContainer)
                    break;
                for (int i = 0; i < _gameDataManager.CurrentWeaponArray.Length; ++i)
                {
                    if (_gameDataManager.CurrentWeaponArray[i] == 0)
                    {
                        _gameDataManager.CurrentWeaponArray[i] = (int)weaponEnum;
                        WeaponProductManager.Instance.currentWeaponMenuContent.SetCurrentWeaponContent(i, weaponEnum);
                        if (i == 2)
                        {
                            WeaponProductManager.Instance.IsFullCurrentWeaponContainer = true;
                        }
                        break;
                    }
                }
                _gameDataManager.GameData.weaponOwnStateArray[(int)weaponEnum] = (int)WeaponOwnState.Used; 
                weaponOwnState = WeaponOwnState.Used;
                buyButton.SetWeaponData(WeaponOwnState.Used);
                GameDataManager.Instance.SaveData();
                break;
        }
    }
}
