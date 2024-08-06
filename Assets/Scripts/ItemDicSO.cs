using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "Item/ItemDicSO", order = 3)]
public class ItemDicSO : ScriptableObject
{
    [Header("������")]
    public ItemSO mainItem;

    [Header("���۹�")]
    public List<ItemSO> madeItems;

    [Header("���չ�")]
    public List<ItemMergeSO> useItems;

}
