using System;
using System.Linq;
using UnityEngine;

public class CurrentWeaponMenuContent : MonoBehaviour
{
    //Managements
    private GameDataManager _gameDataManager;
    
    [SerializeField] private CurrentWeaponContent[] _currentWeaponContents;
    
    
    private void Awake()
    {
        _gameDataManager = GameDataManager.Instance;
        
        //데이터 불러오는 거
        _gameDataManager.LoadData();
        //_currentWeaponContents = new CurrentWeaponContent[_gameDataManager.CurrentWeaponArray.Length];
        for (int i = 0; i < _currentWeaponContents.Length; ++i)
        {
            SetCurrentWeaponContent(i, (WeaponEnum)_gameDataManager.CurrentWeaponArray[i]);//------------------------
        }

        WeaponProductManager.Instance.IsFullCurrentWeaponContainer = _gameDataManager.CurrentWeaponArray.All(x => x != 0);
    }
    
    public void SetCurrentWeaponContent(int index, WeaponEnum weaponEnum)
    {
        try
        {
            _currentWeaponContents[index].WeaponEnum = weaponEnum;
        }
        catch (IndexOutOfRangeException e)
        {
            Debug.LogError($"Weapon Index is wrong! {e}");
        }
    }
}
