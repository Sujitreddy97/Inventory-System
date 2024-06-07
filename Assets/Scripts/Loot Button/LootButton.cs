using UnityEngine;
using UnityEngine.UI;

public class LootButton : MonoBehaviour
{
    [SerializeField] private InventoryService inventoryService;
    private Button button;
    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
        EventService.Instance.OnWeightFullEvent.AddListener(DisableButton);
    }

    private void OnDestroy()
    {
        EventService.Instance.OnWeightFullEvent.RemoveListener(DisableButton);
    }

    public void OnButtonClick()
    {
        AddItems();
    }

    private void AddItems()
    {
        if (inventoryService != null)
        {
            inventoryService.AddItems();
        }
    }

    private void DisableButton()
    {
        button.interactable = false;
    }
}
