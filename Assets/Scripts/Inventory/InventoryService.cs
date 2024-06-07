using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryService
{
    private ItemListScriptableObject itemsData;
    private InventoryView inventoryView;
    private InventoryController inventoryController;
    private InventoryModel inventoryModel;

    public InventoryService(ItemListScriptableObject _itemsData, InventoryView _inventoryView)
    {
        this.itemsData = _itemsData;
        this.inventoryView = _inventoryView;
        inventoryModel = new InventoryModel();
        inventoryController = new InventoryController(inventoryModel, inventoryView);
        EventService.Instance.OnSellItemEvent.AddListener(RemoveSelectedItem);
        EventService.Instance.OnBuyItemEvent.AddListener(AddItems);
    }
    ~InventoryService()
    {
        EventService.Instance.OnSellItemEvent.AddListener(RemoveSelectedItem);
        EventService.Instance.OnBuyItemEvent.AddListener(AddItems);
    }

    public void AddItems()
    {
        int quantity = Random.Range(1, 5);
        inventoryController.AddItems(itemsData.GetRandomItemData(), quantity);
    }

    public void AddItems(ItemScriptableObject _itemData, int _quanity)
    {
        inventoryController.AddItems(_itemData, _quanity);
        inventoryController.ReduceCoins(_itemData.buyingPrice * _quanity);
    }

    public void RemoveSelectedItem(ItemScriptableObject _itemData, int _quantity)
    {
        inventoryController.RemoveSelectedItem(_itemData, _quantity);
    }

    public InventoryController GetInventoryController()
    {
        return inventoryController;
    }
}
