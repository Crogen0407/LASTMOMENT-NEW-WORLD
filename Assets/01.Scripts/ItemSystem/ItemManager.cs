using System;
using AYellowpaper.SerializedCollections;
using Crogen.ObjectPooling;
using Crogen.PowerfulInput;
using UnityEngine;
using UnityEngine.Events;

public class ItemManager : MonoSingleton<ItemManager>
{
    //Managements
    private UIManager _uiManager;

    private InputReader _inputReader;
    
    public ItemType[] currentItem = new ItemType[3];
    [SerializeField] private LayerMask _whatIsItem;
    [SerializeField] private SerializedDictionary<ItemType, UnityEvent> _itemEffectDictionary;
    [SerializeField] private ItemsDataSO _itemsData;
    
    private void Awake()
    {
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
    
    public void HandleUseItem(int itemIndex)
    {
        _itemEffectDictionary[currentItem[itemIndex]]?.Invoke();
        _uiManager.UpdateItemIcon(itemIndex, ItemType.None);
    }
}