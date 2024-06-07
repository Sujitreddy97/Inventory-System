using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameService : MonoBehaviour
{
    public static GameService Instance { get; private set; }
    public InventoryService inventoryService { get; private set; }
    public ShopService shopService { get; private set; }

    [SerializeField] private ItemListScriptableObject inventoryItemsData;
    [SerializeField] private InventoryView inventoryView;

    [SerializeField] private ItemListScriptableObject shopItemList;
    [SerializeField] private ShopView shopView;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        inventoryService = new InventoryService(inventoryItemsData, inventoryView);
        shopService = new ShopService(shopItemList, shopView);
    }
}
