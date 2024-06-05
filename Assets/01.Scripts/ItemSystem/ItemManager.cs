using System;
using System.Collections.Generic;
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
    private Dictionary<ItemType, ItemEffect> _itemEffectDictionary;
    [SerializeField] private ItemsDataSO _itemsData;
    
    private void Awake()
    {
        _itemEffectDictionary = new Dictionary<ItemType, ItemEffect>();

        foreach (ItemType value in Enum.GetValues(typeof(ItemType)))
        {
            _itemEffectDictionary.Add(value, transform.Find(value.ToString()).GetComponent<ItemEffect>());
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
    }
}