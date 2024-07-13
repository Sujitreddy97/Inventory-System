using System.Collections.Generic;

public class InventoryModel
{
    private InventoryController inventoryController;
    private Dictionary<ItemScriptableObject, int> items = new Dictionary<ItemScriptableObject, int>();
    private int maxWeight = 500;
    private int coins;
    private int currentWeight;

    public int GetCoins() => coins;

    public int GetMaxWeight() => maxWeight;

    public int GetCurrentWeight() => currentWeight;

    public void SetInventoryController(InventoryController inventoryController)
    {
        this.inventoryController = inventoryController;
    }

    public void AddItem(ItemScriptableObject _itemdata, int _quantity, int _totalWeightToAdd)
    {
        if (items.ContainsKey(_itemdata))
        {
            items[_itemdata] += _quantity;
        }
        else
        {
            items[_itemdata] = _quantity;
        }
        currentWeight += _totalWeightToAdd;
    }


    public void RemoveItem(ItemScriptableObject itemData, int _quantity)
    {
        if (items.ContainsKey(itemData))
        {
            items[itemData] -= _quantity;

            if (items[itemData] <= 0)
            {
                items.Remove(itemData);
            }

            currentWeight -= itemData.weight * _quantity;
            coins += itemData.sellingPrice * _quantity;
        }
    }

    public int GetItemQuantity(ItemScriptableObject itemData)
    {
        if (CheckItem(itemData))
        {
            return items[itemData];
        }
        return 0;
    }

    public bool CheckItem(ItemScriptableObject itemData)
    {
        return items.ContainsKey(itemData);
    }

    public bool CanAddItem(int itemWeight)
    {
        return currentWeight + itemWeight <= maxWeight;
    }

    public void SubtractCoins(int amount)
    {
        coins -= amount;
    }
}
