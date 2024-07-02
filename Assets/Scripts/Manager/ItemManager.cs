using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviourSingleton<ItemManager>
{

    public void AddItem(ItemSO item, int i)
    {
        if(SaveManager.Instance.itemMap.ContainsKey(item)) 
        {
            SaveManager.Instance.itemMap[item] += i;
            if (SaveManager.Instance.itemMap[item] <= 0)
                SaveManager.Instance.itemMap.Remove(item);
        }
        else if(i > 0)
        {
            SaveManager.Instance.itemMap.Add(item, i);
        }
    }

    public int GetItem(ItemSO item)
    {
        if(SaveManager.Instance.itemMap.ContainsKey(item))
            return SaveManager.Instance.itemMap[item];
        else
            return 0;
    }
}
