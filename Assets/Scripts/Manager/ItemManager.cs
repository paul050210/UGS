using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviourSingleton<ItemManager>
{
    [SerializeField] private ItemSO[] itemSOs;


    public void AddItem(Item item, int i)
    {
        if(SaveManager.Instance.itemMap.ContainsKey(item)) 
        {
            Debug.Log("����");
            SaveManager.Instance.itemMap[item] = Mathf.Max(0, i);
        }
        else
        {
            Debug.Log("�߰�");
            SaveManager.Instance.itemMap.Add(item, Mathf.Max(0, i));
        }
        SaveManager.Instance.SaveItemData();
    }

    public int GetItem(Item item)
    {
        if(SaveManager.Instance.itemMap.ContainsKey(item))
            return SaveManager.Instance.itemMap[item];
        else
            return 0;
    }

    public void ResetMap()
    {
        for(int i = 0; i<itemSOs.Length; i++) 
        {
            AddItem(itemSOs[i].item, 1);
        }
    }

    public Sprite GetItemSprite(Item item)
    {
        for(int i = 0; i < itemSOs.Length; i++) 
        {
            if (itemSOs[i].item.Equals(item))
                return itemSOs[i].sprite;
        }

        Debug.Log("return null");
        return null;
    }
}
