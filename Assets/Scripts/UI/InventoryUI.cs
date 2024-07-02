using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private ItemSO[] itemSOs;

    private void Start()
    {
        SaveManager.Instance.LoadItemData();
        //foreach(ItemSO itemSO in itemSOs) 
        //{
        //    Debug.Log($"{itemSO.item.itemName}: {ItemManager.Instance.GetItem(itemSO.item)}");
        //}

        foreach(var pair in SaveManager.Instance.itemList.items)
        {
            Debug.Log($"{pair.Key.itemName}: {pair.Value}");
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            ItemManager.Instance.AddItem(itemSOs[0].item, 1);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            ItemManager.Instance.AddItem(itemSOs[1].item, 1);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            ItemManager.Instance.AddItem(itemSOs[2].item, 1);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ItemManager.Instance.AddItem(itemSOs[3].item, 1);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            ItemManager.Instance.AddItem(itemSOs[4].item, 1);
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SaveManager.Instance.LoadItemData();

        }
    }
}
