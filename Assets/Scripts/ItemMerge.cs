using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum ItemMergeState
{
    None,
    Merge,
    Decom
}

public class ItemMerge : MonoBehaviour
{
    [SerializeField] private Button doneButton;
    [SerializeField] private Button tabeltButton;

    [SerializeField] private Transform[] itemPopUps;

    private ItemMergeSO[] itemMergeSOs;
    private ItemDecomSO[] itemDecomSOs;
    private ItemMergeState mergeState = ItemMergeState.None;
    private InventoryUI inventoryUI;

    private void Start()
    {
        inventoryUI = GetComponent<InventoryUI>();
        
        itemMergeSOs = Resources.LoadAll<ItemMergeSO>("SO/ItemMerge");
        itemDecomSOs = Resources.LoadAll<ItemDecomSO>("SO/ItemDecom");

        doneButton.onClick.AddListener(OnClickDone);
    }

    private void OnClickDone()
    {
        if (mergeState == ItemMergeState.None) return;

        if(mergeState == ItemMergeState.Merge) 
        {
            Merge(inventoryUI.GetToMerge());
        }
        else
        {
            Decom(inventoryUI.GetToDecom());
        }
        
    }

    private void Merge(Item[] itemArr)
    {
        if (itemArr == null) return;
        ItemSO merged = null;
        for(int i = 0; i< itemMergeSOs.Length; i++)
        {
            merged = itemMergeSOs[i].ReturnMergeItem(itemArr);
            if (merged != null)
                break;
        }

        if (merged != null)
        {
            
            for(int i = 0; i< itemArr.Length; i++)
            {
                int cnt = ItemManager.Instance.GetItem(itemArr[i]);
                ItemManager.Instance.AddItem(itemArr[i], cnt-1);
            }
            int n = ItemManager.Instance.GetItem(merged.item);
            ItemManager.Instance.AddItem(merged.item, n+1);

            itemPopUps[0].gameObject.SetActive(true);
            itemPopUps[0].GetChild(1).GetChild(0).GetComponent<Image>().sprite = merged.sprite;
            itemPopUps[0].GetChild(1).GetChild(0).GetComponent<Image>().color = new Color(255f, 255f, 255f, 1f);
            itemPopUps[0].GetChild(2).GetComponent<Text>().text = merged.item.itemName;
            itemPopUps[0].GetChild(3).GetComponent<Text>().text = merged.item.itemDesc;

        }
        else
        {
            itemPopUps[0].gameObject.SetActive(true);
            itemPopUps[0].GetChild(1).GetChild(0).GetComponent<Image>().sprite = null;
            itemPopUps[0].GetChild(1).GetChild(0).GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
            itemPopUps[0].GetChild(2).GetComponent<Text>().text = "합성실패(임시)";
            itemPopUps[0].GetChild(3).GetComponent<Text>().text = "합성실패(임시)";
        }
        tabeltButton.onClick.Invoke();
    }

    private void Decom(Item item)
    {
        if (item == null) return;
        ItemSO[] items = null;
        for(int i = 0; i<itemDecomSOs.Length; i++)
        {
            items = itemDecomSOs[i].ReturnDecomItem(item);
            if (items != null)
                break;
        }

        if (items != null)
        {
            int cnt = ItemManager.Instance.GetItem(item);
            ItemManager.Instance.AddItem(item, cnt - 1);
            
            for (int i = 0; i<items.Length; i++)
            {
                int n = ItemManager.Instance.GetItem(items[i].item);
                ItemManager.Instance.AddItem(items[i].item, n + 1);

                itemPopUps[i].gameObject.SetActive(true);
                itemPopUps[i].GetChild(1).GetChild(0).GetComponent<Image>().sprite = items[i].sprite;
                itemPopUps[0].GetChild(1).GetChild(0).GetComponent<Image>().color = new Color(255f, 255f, 255f, 1f);
                itemPopUps[i].GetChild(2).GetComponent<Text>().text = items[i].item.itemName;
                itemPopUps[i].GetChild(3).GetComponent<Text>().text = items[i].item.itemDesc;
            }
        }
        else
        {
            itemPopUps[0].gameObject.SetActive(true);
            itemPopUps[0].GetChild(1).GetChild(0).GetComponent<Image>().sprite = null;
            itemPopUps[0].GetChild(1).GetChild(0).GetComponent<Image>().color = new Color(0f,0f,0f,0f);
            itemPopUps[0].GetChild(2).GetComponent<Text>().text = "분해실패(임시)";
            itemPopUps[0].GetChild(3).GetComponent<Text>().text = "분해실패(임시)";
        }

        tabeltButton.onClick.Invoke();
    }

    public void SetMergeState(ItemMergeState state)
    {
        mergeState = state;
    }

    public int GetMaxSelect()
    {
        if (mergeState == ItemMergeState.Decom)
            return 1;
        else
            return 5;
    }
}
