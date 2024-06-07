using UnityEngine;
using UnityEngine.UI;

public class LootButton : MonoBehaviour
{
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
        if (GameService.Instance.inventoryService != null)
        {
            GameService.Instance.inventoryService.AddItems();
        }
    }

    private void DisableButton()
    {
        button.interactable = false;
    }
}
