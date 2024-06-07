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

    public void AddItem(ItemScriptableObject _itemdata, int _quantity)
    {
        if (!CanAddItem(_itemdata.weight) || currentWeight >= maxWeight)
        {
            EventService.Instance.OnWeightFullEvent.InvokeEvent();
            return;
        }

        if (items.ContainsKey(_itemdata))
        {
            items[_itemdata] += _quantity;
        }
        else
        {
            items[_itemdata] = 1;
        }
        currentWeight += _itemdata.weight;
        //coins-= _itemdata.buyingPrice;
        EventService.Instance.OnAddItemEvent.InvokeEvent();//update weight(increase)
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
            EventService.Instance.OnRemoveItemEvent.InvokeEvent();//update weight(decrease)
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
