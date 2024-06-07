using System.Collections.Generic;
using UnityEngine;

public class ShopModel
{
    private ShopController shopController;
    private Dictionary<ItemScriptableObject, int> items = new Dictionary<ItemScriptableObject, int>();

    public void SetShopController(ShopController _shopController)
    {
        this.shopController = _shopController;
    }

    public void AddItem(ItemScriptableObject _itemData, int _quantity)
    {
        if (items.ContainsKey(_itemData))
        {
            items[_itemData] += _quantity;
        }
        else
        {
            items[_itemData] = _quantity;
        }
    }

    public void RemoveItem(ItemScriptableObject _itemData, int _quantity)
    {
        if (items.ContainsKey(_itemData))
        {
            items[_itemData] -= _quantity;

            if (items[_itemData] <= 0)
            {
                items.Remove(_itemData);
            }
        }
    }

    public int GetItemQuantity(ItemScriptableObject _itemData)
    {
        if (CheckItem(_itemData))
        {
            return items[_itemData];
        }
        return 0;
    }

    public bool CheckItem(ItemScriptableObject _itemData)
    {
        return items.ContainsKey(_itemData);
    }

    public Dictionary<ItemScriptableObject, int> GetItems()
    {
        return items;
    }
}
