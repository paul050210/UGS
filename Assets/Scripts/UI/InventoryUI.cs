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

        Debug.Log(SaveManager.Instance.itemMap.Count);
        foreach(var p in SaveManager.Instance.itemMap)
        {
            Debug.Log($"{p.Key.itemName},{p.Key.type}: {p.Value}");
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
            Debug.Log(SaveManager.Instance.itemMap.ContainsKey(itemSOs[0].item));

        }
    }
}
