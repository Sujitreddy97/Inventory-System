using UnityEngine;

public class ShopController
{
    private ShopModel shopModel;
    private ShopView shopView;

    public ShopController(ShopModel _shopModel, ShopView _shopView)
    {
        this.shopModel = _shopModel;
        this.shopView = _shopView;
        _shopModel.SetShopController(this);
        _shopView.SetShopController(this);
    }

    public void AddItems(ItemScriptableObject _itemData, int _quantity)
    {
        shopModel.AddItem(_itemData, _quantity);
        AddItemToShopView(_itemData);
    }

    public void RemoveItems(ItemScriptableObject _itemData, int _quantity)
    {
        shopModel.RemoveItem(_itemData, _quantity);
        shopView.RemoveItemFromPanel(_itemData);
    }

    public int GetItemQuantity(ItemScriptableObject _itemData)
    {
        return shopModel.GetItemQuantity(_itemData);
    }

    private void AddItemToShopView(ItemScriptableObject _itemData)
    {
        int quantity = GetItemQuantity(_itemData);

        if (quantity > 0 && shopView.HasItemControllerKey(_itemData))
        {
            shopView.UpdateItemInPanel(_itemData, quantity);
        }
        else
        {
            shopView.AddItemToPanel(_itemData, quantity);
        }
    }

    private void RemoveItemFromShopView(ItemScriptableObject _itemData)
    {
        shopView.RemoveItemFromPanel(_itemData);
    }
}
