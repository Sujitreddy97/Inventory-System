using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopService
{
    private ItemListScriptableObject itemList;
    private ShopView shopView;
    private ShopModel shopModel;
    private ShopController shopController;

    public ShopService(ItemListScriptableObject _itemsList, ShopView _shopView)
    {
        this.itemList = _itemsList;
        this.shopView = _shopView;
        shopModel = new ShopModel();
        shopController = new ShopController(shopModel, shopView);
        AddItems(itemList.GetItemData());

        EventService.Instance.OnBuyItemEvent.AddListener(RemoveSelectedItems);
        EventService.Instance.OnSellItemEvent.AddListener(AddItems);
    }

    ~ShopService()
    {
        EventService.Instance.OnBuyItemEvent.RemoveListener(RemoveSelectedItems);
        EventService.Instance.OnBuyItemEvent.RemoveListener(AddItems);
    }

    private void AddItems(List<ItemScriptableObject> _itemData)
    {
        if (_itemData == null || _itemData.Count == 0)
        {
            Debug.LogError("No items found in the item list.");
            return;
        }

        foreach (var _item in _itemData)
        {
            int randomQuantity = Random.Range(1, _itemData.Count);
            shopController.AddItems(_item, randomQuantity);
        }
    }

    public void AddItems(ItemScriptableObject _itemData, int _quantity)
    {
        shopController.AddItems(_itemData, _quantity);
    }

    public void RemoveSelectedItems(ItemScriptableObject _itemData, int _quantity)
    {
        shopController.RemoveItems(_itemData, _quantity);
    }
}
