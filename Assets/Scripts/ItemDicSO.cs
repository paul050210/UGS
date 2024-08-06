using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "Item/ItemDicSO", order = 3)]
public class ItemDicSO : ScriptableObject
{
    [Header("아이템")]
    public ItemSO mainItem;

    [Header("제작법")]
    public List<ItemSO> madeItems;

    [Header("조합법")]
    public List<ItemMergeSO> useItems;

}
