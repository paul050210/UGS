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

    private List<ItemMergeSO> itemMergeSOs = new List<ItemMergeSO>();
    private ItemMergeState mergeState = ItemMergeState.None;
    private InventoryUI inventoryUI;

    private void Start()
    {
        inventoryUI = GetComponent<InventoryUI>();

        ItemMergeSO so;
        for (int i = 0; i < itemMergeCnt; i++)
        {
            so = Resources.Load<ItemMergeSO>($"SO/itemMerge{i}");
            itemMergeSOs.Add(so);
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
            itemPopUp.SetActive(true);
            itemImg.sprite = merged.sprite;
            nameTxt.text = merged.item.itemName;
            descriptTxt.text = merged.item.itemDesc;
        }
        else
        {

        }
        tabeltButton.onClick.Invoke();
    }

    public void SetMergeState(ItemMergeState state)
    {
        mergeState = state;
    }
}
