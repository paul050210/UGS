using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviourSingleton<ItemManager>
{
    private ItemSO[] itemSOs;
    [HideInInspector] public bool canSelect = false;

    private void Awake()
    {
        // TODO: itemSOs�� ���ҽ��������� �ҷ����°ɷ� ����
        itemSOs = Resources.LoadAll<ItemSO>("SO/Item");
    }


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
        // ���߿� �ּ� ��������ߵ�
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
            SaveManager.Instance.itemDicMap.Add(itemSOs[i], false);
        }
    }
}
