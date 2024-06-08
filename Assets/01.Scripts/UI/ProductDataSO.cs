using System;
using AYellowpaper.SerializedCollections;
using UnityEngine;

[Serializable]
public struct WeaponProductData
{
    public Sprite weaponIconSprite;
    public string weaponName;
    public int weaponPrice;
    [TextArea]
    public string description;
}

[CreateAssetMenu(menuName = "SO/UI/ProductData")]
public class ProductDataSO : ScriptableObject
{
    public SerializedDictionary<WeaponEnum, WeaponProductData> weaponProductDataDictionary;

    private void Reset()
    {
        weaponProductDataDictionary = new SerializedDictionary<WeaponEnum, WeaponProductData>(); 
        foreach (WeaponEnum value in Enum.GetValues(typeof(WeaponEnum)))
        {
            weaponProductDataDictionary.Add(value, new WeaponProductData());
        }
    }
}
