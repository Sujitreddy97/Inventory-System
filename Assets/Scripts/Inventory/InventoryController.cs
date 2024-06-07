using System.Collections.Generic;
using Unity.VisualScripting;

public class InventoryController
{
    private InventoryModel inventoryModel;
    private InventoryView inventoryView;

/*    private void OnEnable()
    {
        EventService.Instance.OnBuyReduceCoins.AddListener(ReduceCoins);
    }

    private void OnDisable()
    {
        EventService.Instance.OnBuyReduceCoins.RemoveListener(ReduceCoins);
    }*/

    public InventoryController(InventoryModel _inventoryModel, InventoryView _inventoryView)
    {
        this.inventoryModel = _inventoryModel;
        this.inventoryView = _inventoryView;
        inventoryModel.SetInventoryController(this);
        inventoryView.SetInventoryController(this);
    }

    public void AddItems(ItemScriptableObject _itemData, int _quantity)
    {
        inventoryModel.AddItem(_itemData, _quantity);
        inventoryView.AddItemToPanel(_itemData);
    }

    public void RemoveSelectedItem(ItemScriptableObject _itemData, int _quantity)
    {
        inventoryModel.RemoveItem(_itemData, _quantity);
        inventoryView.RemoveItemFromPanel(_itemData);
    }

    public InventoryModel GetInventoryModel()
    {
        return inventoryModel;
    }

    public int GetCurrentWeight()
    {
        return inventoryModel.GetCurrentWeight();
    }

    public int GetMaxWeight()
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
}
