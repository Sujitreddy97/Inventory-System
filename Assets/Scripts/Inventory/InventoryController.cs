using System.Collections.Generic;
using Unity.VisualScripting;

public class InventoryController
{
    private InventoryModel inventoryModel;
    private InventoryView inventoryView;

    public InventoryController(InventoryModel _inventoryModel, InventoryView _inventoryView)
    {
        this.inventoryModel = _inventoryModel;
        this.inventoryView = _inventoryView;
        inventoryModel.SetInventoryController(this);
        inventoryView.SetInventoryController(this);
    }

    public void AddItems(ItemScriptableObject _itemData, int _quantity)
    {
        int totalWeightToAdd = _itemData.weight * _quantity;

        if (inventoryModel.CanAddItem(totalWeightToAdd))
        {
            inventoryModel.AddItem(_itemData, _quantity, totalWeightToAdd);
            AddItemToInventoryView(_itemData);
            EventService.Instance.OnAddItemEvent.InvokeEvent();
        }
        else
        {
            EventService.Instance.OnWeightFullEvent.InvokeEvent();
            return;
        }
    }

    public void RemoveSelectedItem(ItemScriptableObject _itemData, int _quantity)
    {
        inventoryModel.RemoveItem(_itemData, _quantity);
        RemoveItemFromInventoryView(_itemData);
        EventService.Instance.OnRemoveItemEvent.InvokeEvent();
    }

    public int GetItemQuantity(ItemScriptableObject itemData)
    {
        return inventoryModel.GetItemQuantity(itemData);
    }

    public int GetCurrentWeight()
    {
        return inventoryModel.GetCurrentWeight();
    }

    private int GetMaxWeight()
    {
        return inventoryModel.GetMaxWeight();
    }

    public int GetCoins()
    {
        return inventoryModel.GetCoins();
    }

    public bool CanAfford(int cost)
    {
        return GetCoins() >= cost;
    }

    public bool CanCarryWeight(int weight)
    {
        return GetCurrentWeight() + weight <= GetMaxWeight();
    }

    public void ReduceCoins(int amount)
    {
        inventoryModel.SubtractCoins(amount);
        inventoryView.UpdateCoinsDisplay();
    }

    private void AddItemToInventoryView(ItemScriptableObject _itemData)
    {
        int quantity = GetItemQuantity(_itemData);

        if (quantity > 0 && inventoryView.HasItemControllerKey(_itemData))
        {
            inventoryView.UpdateItemInPanel(_itemData, quantity);
        }
        else
        {
            inventoryView.AddItemToPanel(_itemData, quantity);
        }

    }

    private void RemoveItemFromInventoryView(ItemScriptableObject _itemData)
    {
        inventoryView.RemoveItemFromPanel(_itemData);
    }
}
