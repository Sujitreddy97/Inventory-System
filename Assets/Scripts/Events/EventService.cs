using UnityEngine;

public class EventService
{
    private static EventService instance;
    public static EventService Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new EventService();
            }
            return instance;
        }

    }

    public EventController OnAddItemEvent { get; private set; }
    public EventController OnRemoveItemEvent { get; private set; }
    public EventController OnWeightFullEvent { get; private set; }
    public EventController<ItemScriptableObject, int> OnSellItemEvent { get; private set; }
    public EventController<ItemScriptableObject, int> OnBuyItemEvent { get; private set; }

    public EventService()
    {
        OnAddItemEvent = new EventController();
        OnRemoveItemEvent = new EventController();
        OnWeightFullEvent = new EventController();
        OnSellItemEvent = new EventController<ItemScriptableObject, int>();
        OnBuyItemEvent = new EventController<ItemScriptableObject, int>();
    }

}
