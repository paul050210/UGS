using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "ItemMergeSO")]
public class ItemMergeSO : ScriptableObject
{
    [SerializeField] private ItemSO baseItemA = null;
    [SerializeField] private ItemSO baseItemB = null;
    [SerializeField] private ItemSO mergeItem = null;

    

    public Item ReturnMergeItem(Item itemA, Item itemB)
    {
        if (itemA == null || itemB == null)
        {
            return null;
        }
        
        if(itemA.Equals((Item)baseItemA) && itemB.Equals((Item)baseItemB))
            return mergeItem;

        return null;
    }

}
