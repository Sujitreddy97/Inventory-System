using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item List Scriptable Object", menuName = "Items List")]
public class ItemListScriptableObject : ScriptableObject
{
  public List<ItemScriptableObject> itemsData;

    public List<ItemScriptableObject> GetItemData()
    {
        return itemsData;
    }

    public ItemScriptableObject GetRandomItemData()
    {
        return itemsData[Random.Range(0, itemsData.Count)];
    }
}
