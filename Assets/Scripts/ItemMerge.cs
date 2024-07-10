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

    [SerializeField] private GameObject itemPopUp;
    [SerializeField] private Image itemImg;
    [SerializeField] private Text nameTxt;
    [SerializeField] private Text descriptTxt;

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
            //itemPopUp.SetActive(true);
            //itemImg.sprite = merged.sprite;
            //nameTxt.text = merged.item.itemName;
            //descriptTxt.text = merged.item.itemDesc;

            Transform popup = Instantiate(itemPopUp, transform.parent).transform;
            popup.gameObject.SetActive(true);
            popup.GetChild(0).GetComponent<Image>().sprite = merged.sprite;
            popup.GetChild(1).GetComponent<Text>().text = merged.item.itemName;
            popup.GetChild(2).GetComponent<Text>().text = merged.item.itemDesc;

        }
        else
        {
            itemPopUp.SetActive(true);
            nameTxt.text = "합성실패(임시)";
            descriptTxt.text = "합성실패(임시)";
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
            for(int i = 0; i<items.Length; i++)
            {
                Transform popup = Instantiate(itemPopUp, transform.parent).transform;
                popup.gameObject.SetActive(true);
                popup.GetChild(0).GetComponent<Image>().sprite = items[i].sprite;
                popup.GetChild(1).GetComponent<Text>().text = items[i].item.itemName;
                popup.GetChild(2).GetComponent<Text>().text = items[i].item.itemDesc;
            }
        }
        else
        {
            itemPopUp.SetActive(true);
            nameTxt.text = "분해실패(임시)";
            descriptTxt.text = "분해실패(임시)";
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
