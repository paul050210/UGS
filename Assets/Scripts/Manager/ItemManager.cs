using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviourSingleton<ItemManager>
{

    public void AddItem(Item item, int i)
    {
        if(SaveManager.Instance.itemMap.ContainsKey(item)) 
        {
            SaveManager.Instance.itemMap[item] += i;
            if (SaveManager.Instance.itemMap[item] <= 0)
                SaveManager.Instance.itemMap.Remove(item);

            SaveManager.Instance.SaveItemData();
        }
        else if(i > 0)
        {
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
