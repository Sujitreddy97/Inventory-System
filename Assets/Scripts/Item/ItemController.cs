using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    private ItemScriptableObject itemData;
    private Sprite sprite;
    private ItemsType type;
    private int weight;
    private int buyingPrice;
    private int sellingPrice;
    [TextArea]
    private string description;

    [SerializeField] private Button button;
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI quantityText;
    private int quantity = 0;
    private bool isFromInventory;

 
    private void Start()
    {
        quantityText.text = quantity.ToString();
        button.onClick.AddListener(ShowDiscription);
    }

    public void SetItemData(ItemScriptableObject _itemData, bool _isFromInventory)
    {
        this.itemData = _itemData;
        this.sprite = _itemData.Sprite;
        this.type = _itemData.Type;
        this.weight = _itemData.weight;
        this.buyingPrice = _itemData.buyingPrice;
        this.sellingPrice = _itemData.sellingPrice;
        this.description = _itemData.discription;
        this.isFromInventory = _isFromInventory;

        if (itemImage != null)
        {
            itemImage.sprite = sprite;
        }
    }

    public void SetItemQuantity(int _quantity)
    {
        quantity = _quantity;

        if (quantityText != null)
        {
            quantityText.text = quantity.ToString();
        }
    }

    private void ShowDiscription()
    {
        DiscriptionController.Instance.ShowDiscriptionPanel();
        DiscriptionController.Instance.SetDescriptionData(itemData,sprite, weight, quantity, buyingPrice, sellingPrice, description, isFromInventory);
    }
}
