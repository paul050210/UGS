using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "Item/ItemSO", order = 0)]
public class ItemSO : ScriptableObject
{
    public Item item;
    public Sprite sprite;


    public static implicit operator Item(ItemSO so)
    {
        return so.item;
    }
    
    
}
