using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "ItemMergeSO")]
public class ItemMergeSO : ScriptableObject
{
    [SerializeField] private ItemSO baseItemA;
    [SerializeField] private ItemSO baseItemB;
    [SerializeField] private ItemSO mergeItem;

    

    public Item ReturnMergeItem(Item itemA, Item itemB)
    {
        if (itemA == null || itemB == null) return null;

        return mergeItem;
    }

}
