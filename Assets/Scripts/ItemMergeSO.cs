using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
[CreateAssetMenu(menuName = "ItemMergeSO")]
public class ItemMergeSO : ScriptableObject
{
    [SerializeField] private ItemSO[] baseItems;
    [SerializeField] private ItemSO mergeItem = null;




    public ItemSO ReturnMergeItem(Item[] items)
    {
        if (items.Length != baseItems.Length) return null;
        Item[] inputArr = items.OrderBy(i => i.itemName).ToArray();
        Item[] thisArr = new Item[baseItems.Length];
        for(int i = 0; i<baseItems.Length;i++)
        {
            thisArr[i] = baseItems[i].item;
        }
        thisArr = thisArr.OrderBy(i => i.itemName).ToArray();

        if (Enumerable.SequenceEqual(inputArr, thisArr))
            return mergeItem;

        return null;
    }
}
