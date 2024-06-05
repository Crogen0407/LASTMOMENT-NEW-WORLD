using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/ItemsDataSO")]
public class ItemsDataSO : ScriptableObject
{
    public SerializedDictionary<ItemType, PoolType> ItemPoolDictionary;

}
