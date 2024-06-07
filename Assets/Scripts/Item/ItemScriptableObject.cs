using UnityEngine;

[CreateAssetMenu(fileName = "Item Scriptable Object", menuName = "Items")]
public class ItemScriptableObject : ScriptableObject 
{
    public Sprite Sprite;
    public ItemsType Type;
    public int weight;
    public int buyingPrice;
    public int sellingPrice;
    [TextArea]
    public string discription;
}
