using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviourSingleton<ItemManager>
{
    private ItemSO[] itemSOs;
    private ItemDicSO[] itemDicSOs;
    [HideInInspector] public bool canSelect = false;

    private void Awake()
    {
        itemSOs = Resources.LoadAll<ItemSO>("SO/Item");
        itemDicSOs = Resources.LoadAll<ItemDicSO>("SO/ItemDic");
    }


    public void AddItem(Item item, int i)
    {
        if(SaveManager.Instance.itemMap.ContainsKey(item)) 
        {
            Debug.Log("수정");
            SaveManager.Instance.itemMap[item] = Mathf.Max(0, i);
        }
        else
        {
            Debug.Log("추가");
            SaveManager.Instance.itemMap.Add(item, Mathf.Max(0, i));
        }
        // 나중엔 주석 해제해줘야됨
        //SaveManager.Instance.SaveItemData();
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
        if(itemSOs == null)
        {
            itemSOs = Resources.LoadAll<ItemSO>("SO/Item");
        }
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

    public void AddDic(Item item)
    {
        SaveManager.Instance.itemDicMap[item] = true;
    }

    public void ResetItemDic()
    {
        if (itemSOs == null)
        {
            itemSOs = Resources.LoadAll<ItemSO>("SO/Item");
        }
        for (int i = 0; i < itemSOs.Length; i++)
        {
            SaveManager.Instance.itemDicMap.Add(itemSOs[i], i%2 == 0);
        }
    }

    public List<ItemSO> GetDicData(Item item)
    {
        for(int i = 0; i<itemDicSOs.Length; i++)
        {
            if (itemDicSOs[i].mainItem.item.Equals(item))
            {
                return itemDicSOs[i].madeItems;
            }
        }

        return null;
    }

    public List<ItemMergeSO> GetDicData2(Item item)
    {
        for (int i = 0; i < itemDicSOs.Length; i++)
        {
            if (itemDicSOs[i].mainItem.item.Equals(item))
            {
                return itemDicSOs[i].useItems;
            }
        }

        return null;
    }
}
