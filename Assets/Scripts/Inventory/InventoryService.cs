using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryService : MonoBehaviour
{
    public static InventoryService Instance { get; private set; }

    [SerializeField] private ItemListScriptableObject itemsData;
    [SerializeField] private InventoryView inventoryView;
    private InventoryController inventoryController;
    private InventoryModel inventoryModel;

    private void OnEnable()
    {
        EventService.Instance.OnSellItemEvent.AddListener(RemoveSelectedItem);
        EventService.Instance.OnBuyItemEvent.AddListener(AddItems);
    }

    private void OnDisable()
    {
        EventService.Instance.OnSellItemEvent.AddListener(RemoveSelectedItem);
        EventService.Instance.OnBuyItemEvent.AddListener(AddItems);
    }

    private void Awake()
    {
        if(Instance == null)
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
        inventoryModel = new InventoryModel();
        inventoryController = new InventoryController(inventoryModel, inventoryView);
    }

    public void AddItems()
    {
        int quantity = Random.Range(1, 5);
        inventoryController.AddItems(itemsData.GetRandomItemData(),quantity);
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
