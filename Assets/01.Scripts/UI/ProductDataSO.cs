using System;
using AYellowpaper.SerializedCollections;
using UnityEngine;

[Serializable]
public struct WeaponData
{
    public Sprite weaponIconSprite;
    public string weaponName;
    public int weaponPrice;
}

[CreateAssetMenu(menuName = "SO/UI/ProductData")]
public class ProductDataSO : ScriptableObject
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
