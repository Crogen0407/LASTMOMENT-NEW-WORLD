using System;
using AYellowpaper.SerializedCollections;
using UnityEngine;

[Serializable]
public struct WeaponData
{
    public float coolTime;
    public float attackCount;
}

[CreateAssetMenu(menuName = "SO/WeaponSystem/WeaponData")]
public class WeaponDataSO : ScriptableObject
{
    public SerializedDictionary<WeaponEnum, WeaponData> weaponDataDictionary;
    
    private void Reset()
    {
        weaponDataDictionary = new SerializedDictionary<WeaponEnum, WeaponData>(); 
        foreach (WeaponEnum value in Enum.GetValues(typeof(WeaponEnum)))
        {
            weaponDataDictionary.Add(value, new WeaponData());
        }
    }
}
