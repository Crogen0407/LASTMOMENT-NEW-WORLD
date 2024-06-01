using AYellowpaper.SerializedCollections;
using Crogen.ObjectPooling;
using UnityEngine;

public class ItemManager : MonoSingleton<ItemManager>
{
    private UIManager _uiManager;
    public ItemType[] currentItem = new ItemType[3];
    [SerializeField] private LayerMask _whatIsItem;
    [SerializeField] private SerializedDictionary<ItemType, PoolType> itemEffectDictionary;
    
    private void Awake()
    {
        _uiManager = UIManager.Instance;
    }

    public void PushInItemArray(ItemType itemType)
    {
        for (int i = 0; i < currentItem.Length; ++i)
        {
            if (currentItem[i] == ItemType.None)
            {
                currentItem[i] = itemType;
                _uiManager.UpdateItemIcon(i, itemType);
            }
        }
    }

    public void UseItem(int itemIndex)
    {
        this.Pop(itemEffectDictionary[currentItem[itemIndex]]);
        _uiManager.UpdateItemIcon(itemIndex, ItemType.None);
    }
}