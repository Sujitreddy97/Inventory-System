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
    private Dictionary<ItemScriptableObject, ItemController> itemControllers = new Dictionary<ItemScriptableObject, ItemController>();

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

    public void AddItemToPanel(ItemScriptableObject _itemData)
    {
        if (itemControllers.ContainsKey(_itemData))
        {
            // Update the existing item's quantity
            ItemController itemController = itemControllers[_itemData];
            int quantity = inventoryController.GetInventoryModel().GetItemQuantity(_itemData);
            itemController.SetItemQuantity(quantity);
        }
        else
        {
            // Create a new item button
            GameObject buttonObject = Instantiate(itemButtonPrefab, inventoryGrid);
            ItemController itemController = buttonObject.GetComponent<ItemController>();

            if (itemController != null)
            {
                itemController.SetItemData(_itemData, true);
                itemController.SetItemQuantity(inventoryController.GetInventoryModel().GetItemQuantity(_itemData));
                itemControllers[_itemData] = itemController;
            }
        }
    }

    public void RemoveItemFromPanel(ItemScriptableObject _itemData)
    {
        if (itemControllers.ContainsKey(_itemData))
        {
            ItemController itemController = itemControllers[_itemData];
            int quantity = inventoryController.GetInventoryModel().GetItemQuantity(_itemData);
            itemController.SetItemQuantity(quantity);

            if (quantity == 0)
            {
                itemControllers.Remove(_itemData);
                Destroy(itemController.gameObject);
            }
        }
        
    }

    private void UpdateInventoryWeightDisplay()
    {
        inventoryWeight.text = $"{"WEIGHT: "}{inventoryController.GetCurrentWeight()}/{inventoryController.GetMaxWeight()}";
    }

    public void UpdateCoinsDisplay()
    {
        coinsText.text = $"{"COINS: "}{ inventoryController.GetCoins()}";
    }
}
