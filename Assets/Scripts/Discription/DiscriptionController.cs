using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiscriptionController: MonoBehaviour
{
    public static DiscriptionController Instance { get; private set; }

    [SerializeField] private GameObject descriptionPanel;
    [SerializeField] private GameObject onWeightFullPanel;
    [SerializeField] private GameObject onCoinsNotAvailablePanel;
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI weight;
    [SerializeField] private TextMeshProUGUI price;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI maxItemQuantityText;
    [SerializeField] private TextMeshProUGUI quantityText;
    [SerializeField] private Button quantityIncreaseButton;
    [SerializeField] private Button quantityDecreaseButton;
    [SerializeField] private Button sellButton;
    [SerializeField] private Button buyButton;

    private int quantity;
    private int maxItemQuantity;
    private ItemScriptableObject currentItem;


    private void OnEnable()
    {
        EventService.Instance.OnWeightFullEvent.AddListener(OnInventoryWeightFull);
    }


    private void OnDisable()
    {
        EventService.Instance.OnWeightFullEvent.RemoveListener(OnInventoryWeightFull);
    }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            HideDescription();
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        quantityIncreaseButton.onClick.AddListener(IncreaseQuantity);
        quantityDecreaseButton.onClick.AddListener(DecreaseQuantity);
        sellButton.onClick.AddListener(OnSellButtonClickEvent);
        buyButton.onClick.AddListener(OnBuyButtonClickEvent);
    }

    public void SetDescriptionData(ItemScriptableObject _itemData, Sprite _itemImage, int _weight, int _quantity, int _buyingPrice, int _sellingPrice, string _description, bool _fromInventory)
    {
        if (descriptionPanel == null)
        {
            Debug.LogError("Description panel is not assigned!");
            return;
        }

        descriptionPanel.SetActive(true);

        this.currentItem = _itemData;
        this.itemImage.sprite = _itemImage;
        this.weight.text = $"Weight: {_weight}";
        this.maxItemQuantityText.text = _quantity.ToString();
        this.price.text = $"Coins: {(_fromInventory ? _sellingPrice : _buyingPrice)}";
        this.description.text = _description;
        maxItemQuantity = _quantity;
        quantity = 1;
        quantityText.text = quantity.ToString();
        UpdateButtonInteractivity();

        ToggleSellAndBuyButtons(_fromInventory);
    }


    private void IncreaseQuantity()
    {
        if (quantity < maxItemQuantity)
        {
            quantity++;
            quantityText.text = quantity.ToString();
            UpdateButtonInteractivity();
        }
    }

    private void DecreaseQuantity()
    {
        if (quantity > 1)
        {
            quantity--;
            quantityText.text = quantity.ToString();
            UpdateButtonInteractivity();
        }
    }

    private void UpdateButtonInteractivity()
    {
        quantityIncreaseButton.interactable = quantity < maxItemQuantity;
        quantityDecreaseButton.interactable = quantity > 1;
    }

    private void ToggleSellAndBuyButtons(bool fromInventory)
    {
        sellButton.gameObject.SetActive(fromInventory);
        buyButton.gameObject.SetActive(!fromInventory);
    }

    public void HideDescription()
    {
        descriptionPanel.SetActive(false);
    }

    private void OnSellButtonClickEvent()
    {
        EventService.Instance.OnSellItemEvent.InvokeEvent(currentItem, quantity);
        HideDescription();
    }

    private void OnBuyButtonClickEvent()
    {
        int totalCost = currentItem.buyingPrice * quantity;
        int totalWeight = currentItem.weight * quantity;

        if (GameService.Instance.inventoryService.GetInventoryController().CanAfford(totalCost))
        {
            if (GameService.Instance.inventoryService.GetInventoryController().CanCarryWeight(totalWeight))
            {
                EventService.Instance.OnBuyItemEvent.InvokeEvent(currentItem, quantity);
                HideDescription();
            }
            else
            {
                OnInventoryWeightFull();
            }
        }
        else
        {
            OnCoinsNotAvailablePanel();
        }
    }

    private void OnCoinsNotAvailablePanel()
    {
        onCoinsNotAvailablePanel.SetActive(true);
    }

    private void OnInventoryWeightFull()
    {
        onWeightFullPanel.SetActive(true);
    }
}
