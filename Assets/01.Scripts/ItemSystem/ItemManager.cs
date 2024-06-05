using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Crogen.ObjectPooling;
using Crogen.PowerfulInput;
using UnityEngine;

public class ItemManager : MonoSingleton<ItemManager>
{
    //Managements
    private UIManager _uiManager;

    private InputReader _inputReader;
    
    public ItemType[] currentItem = new ItemType[3];
    [SerializeField] private LayerMask _whatIsItem;
    [SerializeField] private SerializedDictionary<ItemType, ItemEffect> _itemEffectDictionary;
    [SerializeField] private ItemsDataSO _itemsData;
    
    private void Awake()
    {
        _itemEffectDictionary = new SerializedDictionary<ItemType, ItemEffect>();

        foreach (ItemType itemType in Enum.GetValues(typeof(ItemType)))
        {
            if(itemType==ItemType.None) continue;
            string itemName = itemType.ToString();
            try
            {
                ItemEffect skillComponent = GetComponent($"{itemName}Effect") as ItemEffect;
                _itemEffectDictionary.Add(itemType, skillComponent);
            }
            catch (Exception e)
            {
                Debug.LogError($"{itemName} is missing! check item manager");
            }
        }
        
        _uiManager = UIManager.Instance;
        _inputReader = GameManager.Instance.InputReader;

        _inputReader.UseItemEvent += HandleUseItem;
    }

    private void OnDestroy()
    {
        _inputReader.UseItemEvent -= HandleUseItem;
    }

    public void PushInItemArray(ItemType itemType)
    {
        for (int i = 0; i < currentItem.Length; ++i)
        {
            if (currentItem[i] == ItemType.None)
            {
                currentItem[i] = itemType;  
                _uiManager.UpdateItemIcon(i, itemType);
                break;
            }
        }
    }

    public void DropItem(Vector3 position, ItemType itemType)
    {
        if (itemType == ItemType.None) return;
        this.Pop(_itemsData.ItemPoolDictionary[itemType], position, Quaternion.identity);
    }
    
    private void HandleUseItem(int itemIndex)
    {
        _itemEffectDictionary[currentItem[itemIndex]].UseItem();
        _uiManager.UpdateItemIcon(itemIndex, ItemType.None);
        currentItem[itemIndex] = ItemType.None;
    }
}