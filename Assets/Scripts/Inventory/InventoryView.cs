using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    private InventoryController inventoryController;
    [SerializeField] private GameObject itemButtonPrefab;
    [SerializeField] private Transform inventoryGrid;
    [SerializeField] private TextMeshProUGUI inventoryWeight;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private Button lootButton;
    private Dictionary<ItemScriptableObject, ItemController> items = new Dictionary<ItemScriptableObject, ItemController>();

    private void OnEnable()
    {
        EventService.Instance.OnAddItemEvent.AddListener(UpdateInventoryWeightDisplay);
        EventService.Instance.OnRemoveItemEvent.AddListener(UpdateInventoryWeightDisplay);

        EventService.Instance.OnAddItemEvent.AddListener(UpdateCoinsDisplay);
        EventService.Instance.OnRemoveItemEvent.AddListener(UpdateCoinsDisplay);
    }

    private void OnDisable()
    {
        EventService.Instance.OnAddItemEvent.RemoveListener(UpdateInventoryWeightDisplay);
        EventService.Instance.OnRemoveItemEvent.RemoveListener(UpdateInventoryWeightDisplay);

        EventService.Instance.OnAddItemEvent.RemoveListener(UpdateCoinsDisplay);
        EventService.Instance.OnRemoveItemEvent.RemoveListener(UpdateCoinsDisplay);
    }


    public void SetInventoryController(InventoryController inventoryController)
    {
        this.inventoryController = inventoryController;
    }

    public void AddItemToPanel(ItemScriptableObject _itemData, int _quantity)
    {

        // Create a new item button
        GameObject buttonObject = Instantiate(itemButtonPrefab, inventoryGrid);
        ItemController itemController = buttonObject.GetComponent<ItemController>();

        if (itemController != null)
        {
            itemController.SetItemData(_itemData, true);
            itemController.SetItemQuantity(_quantity);
            items[_itemData] = itemController;
        }

    }

    public void RemoveItemFromPanel(ItemScriptableObject _itemData)
    {
        if (items.TryGetValue(_itemData, out ItemController itemController))
        {
            Destroy(itemController.gameObject);
            items.Remove(_itemData);
        }
    }

    public void UpdateItemInPanel(ItemScriptableObject _itemData, int _quantity)
    {
        if (items.TryGetValue(_itemData, out ItemController itemController))
        {
            itemController.SetItemQuantity(_quantity);
        }
    }

    public bool HasItemControllerKey(ItemScriptableObject _itemData)
    {
        return items.ContainsKey(_itemData);
    }

    private void UpdateInventoryWeightDisplay()
    {
        inventoryWeight.text = $"{"WEIGHT: "}{inventoryController.GetCurrentWeight()}";
    }

    public void UpdateCoinsDisplay()
    {
        coinsText.text = $"{"COINS: "}{inventoryController.GetCoins()}";
    }
}
