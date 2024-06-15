using System;
using UnityEngine;

public class CurrentWeaponMenuContent : MonoBehaviour
{
    [SerializeField] private CurrentWeaponContent[] _currentWeaponContents;
    
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
