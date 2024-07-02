using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    private Item item;

    public void SetItem(Item item)
    {
        this.item = item;
        GetComponentInChildren<Image>().sprite = ItemManager.Instance.GetItemSprite(item);
    }

    public void ResetItem()
    {
        item = null;
        GetComponentInChildren<Image>().sprite = null;
    }
}
