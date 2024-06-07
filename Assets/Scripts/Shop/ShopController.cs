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
        shopView.AddItemToPanel(_itemData);
    }

    public void RemoveItems(ItemScriptableObject _itemData, int _quantity)
    {
        shopModel.RemoveItem(_itemData, _quantity);
        shopView.RemoveItemFromPanel(_itemData);
    }

    public ShopModel GetShopModel()
    {
        return shopModel;
    }
}
