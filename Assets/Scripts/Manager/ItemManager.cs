using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviourSingleton<ItemManager>
{

    public void AddItem(Item item, int i)
    {
        if(SaveManager.Instance.itemMap.ContainsKey(item)) 
        {
            Debug.Log("수정");
            SaveManager.Instance.itemMap[item] = Mathf.Max(0, SaveManager.Instance.itemMap[item] + i);
            SaveManager.Instance.SaveItemData();
        }
        else if(i > 0)
        {
            Debug.Log("추가");
            SaveManager.Instance.itemMap.Add(item, i);
            SaveManager.Instance.SaveItemData();
        }
    }

    public int GetItem(Item item)
    {
        if(SaveManager.Instance.itemMap.ContainsKey(item))
            return SaveManager.Instance.itemMap[item];
        else
            return 0;
    }
}
