using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "ItemDecomSO")]
public class ItemDecomSO : ScriptableObject
{
    [SerializeField] private ItemSO baseItem;
    [SerializeField] private ItemSO[] decomItems;

    public ItemSO[] ReturnDecomItem(Item item)
    {
        if(item.Equals(baseItem.item))
        {
            return decomItems;
        }

        return null;
    }
}
