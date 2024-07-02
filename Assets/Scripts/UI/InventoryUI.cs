using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public List<ItemSlotUI> slots; 

    private void Start()
    {
        SaveManager.Instance.LoadItemData();

        foreach(var p in SaveManager.Instance.itemMap)
        {
            slots[0].SetItem(p.Key);
            break;
        }
    }

}
