using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameService : GenericMonoSingleton<GameService>
{
    public InventoryService inventoryService { get; private set; }
    public ShopService shopService { get; private set; }

    [Header("Inventory Service")]
    [SerializeField] private ItemListScriptableObject inventoryItemsData;
    [SerializeField] private InventoryView inventoryView;

    [Header("Shop Service")]
    [SerializeField] private ItemListScriptableObject shopItemList;
    [SerializeField] private ShopView shopView;

    private void Start()
    {
        inventoryService = new InventoryService(inventoryItemsData, inventoryView);
        shopService = new ShopService(shopItemList, shopView);
    }

    private void OnDisable()
    {
        inventoryService?.OnDisable();
        shopService.OnDisable();
    }
}
