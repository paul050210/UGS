using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemMergeSO : ScriptableObject
{
    [SerializeField] private Item baseItemA;
    [SerializeField] private Item baseItemB;
    [SerializeField] private Item mergeItem;

    public ItemMergeSO(Item baseItemA, Item baseItemB, Item mergeItem)
    {
        this.baseItemA = baseItemA;
        this.baseItemB = baseItemB;
        this.mergeItem = mergeItem;
    }

    public Item ReturnMergeItem(Item itemA, Item itemB)
    {
        if(itemA==null || itemB == null) return null;
        if((itemA.Equals(baseItemA) && itemB.Equals(baseItemB)) || (itemA.Equals(baseItemB) && itemB.Equals(baseItemA)))
        {
            return mergeItem;
        }

        return null;
    }

}


public class ItemMergeTest : MonoBehaviour
{
    private ItemMergeSO itemMergeSO;
    private Item itemA;
    private Item itemB;
    private Item itemC;

    private void Start()
    {
        itemA = new Item();
        itemA.type = ItemType.potion;
        itemA.itemName = "A";

        itemB = new Item();
        itemB.type = ItemType.potion;
        itemB.itemName = "B";

        itemC = new Item();
        itemC.type = ItemType.potion;
        itemC.itemName = "C";

        itemMergeSO = new ItemMergeSO(itemA, itemB, itemC);

        Item testItem = itemMergeSO.ReturnMergeItem(itemA, null);
        if(testItem == null) { Debug.Log("Null"); }
        else { Debug.Log(testItem.itemName); }
    }


    public Item ItemMerge(Item itemA, Item itemB)
    {
        return null;
    }

}
