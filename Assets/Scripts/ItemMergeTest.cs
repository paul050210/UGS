using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ItemMergeTest : MonoBehaviour
{

    private List<ItemMergeSO> itemMergeSOs= new List<ItemMergeSO>();
    public ItemSO so1;
    public ItemSO so2;

    private void Start()
    {
        ItemMergeSO so;
        for(int i = 0; i<1; i++)
        {
            so = Resources.Load<ItemMergeSO>($"SO/itemMerge{i}");
            itemMergeSOs.Add(so);
        }
        
        Debug.Log(ItemMerge(so1, so2).itemName);
    }


    public Item ItemMerge(Item itemA, Item itemB)
    {
        Item returnItem = null;
        foreach(ItemMergeSO so in itemMergeSOs)
        {
            returnItem = so.ReturnMergeItem(itemA, itemB);
            if (returnItem != null) break;
        }

        return returnItem;
    }

}
