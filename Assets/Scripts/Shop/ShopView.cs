using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopView : MonoBehaviour
{
    private ShopController shopController;
    [SerializeField] private GameObject itemButtonPrefab;
    [SerializeField] private Transform inventoryGrid;
    [SerializeField] private Button allItemsButton;
    [SerializeField] private Button materialsButton;
    [SerializeField] private Button consumablesButton;
    [SerializeField] private Button treasureButton;
    [SerializeField] private Button weaponsButton;
    private Dictionary<ItemScriptableObject, ItemController> items = new Dictionary<ItemScriptableObject, ItemController>();
    private Dictionary<ItemScriptableObject, GameObject> itemButtons = new Dictionary<ItemScriptableObject, GameObject>();

    private void Start()
    {
        allItemsButton.onClick.AddListener(() => FilterItems(null));
        materialsButton.onClick.AddListener(() => FilterItems(ItemsType.Materials));
        consumablesButton.onClick.AddListener(() => FilterItems(ItemsType.Consumables));
        treasureButton.onClick.AddListener(() => FilterItems(ItemsType.Treasure));
        weaponsButton.onClick.AddListener(() => FilterItems(ItemsType.Weapons));
    }

    public void SetShopController(ShopController _shopController)
    {
        this.shopController = _shopController;
    }


    public void AddItemToPanel(ItemScriptableObject _itemData, int _quantity)
    {

        GameObject buttonObject = Instantiate(itemButtonPrefab, inventoryGrid);
        ItemController itemController = buttonObject.GetComponent<ItemController>();

        if (itemController != null)
        {
            itemController.SetItemData(_itemData, false);
            itemController.SetItemQuantity(_quantity);
            items[_itemData] = itemController;
            itemButtons[_itemData] = buttonObject;
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

    private void FilterItems(ItemsType? itemType)
    {
        foreach (var itemButton in itemButtons)
        {
            itemButton.Value.SetActive(itemType == null || itemButton.Key.Type == itemType);
        }
    }
}
