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

    [SerializeField] private int itemMergeCnt;
    [SerializeField] private int itemDecomCnt;

    private List<ItemMergeSO> itemMergeSOs = new List<ItemMergeSO>();
    private List<ItemDecomSO> itemDecomSOs = new List<ItemDecomSO>();
    private ItemMergeState mergeState = ItemMergeState.None;
    private InventoryUI inventoryUI;

    private void Start()
    {
        inventoryUI = GetComponent<InventoryUI>();

        ItemMergeSO so;
        ItemDecomSO so2;
        for (int i = 0; i < itemMergeCnt; i++)
        {
            so = Resources.Load<ItemMergeSO>($"SO/itemMerge{i}");
            itemMergeSOs.Add(so);
        }
        for(int i = 0; i< itemDecomCnt; i++)
        {
            so2 = Resources.Load<ItemDecomSO>($"SO/itemDecom{i}");
            itemDecomSOs.Add(so2);
        }
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
        for(int i = 0; i< itemMergeSOs.Count; i++)
        {
            merged = itemMergeSOs[i].ReturnMergeItem(itemArr);
            if (merged != null)
                break;
        }

        if (merged != null)
        {
            //사용된 아이템제거, 획득한 아이템 저장 추가해야됨
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
        for(int i = 0; i<itemDecomSOs.Count; i++)
        {
            items = itemDecomSOs[i].ReturnDecomItem(item);
            if (items != null)
                break;
        }

        if (items != null)
        {
            int cnt = ItemManager.Instance.GetItem(item);
            ItemManager.Instance.AddItem(item, cnt - 1);
            //사용된 아이템제거, 획득한 아이템 저장 추가해야됨
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
