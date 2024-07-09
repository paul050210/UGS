using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    private Item item;
    private Button button;
    private Text itemText;
    private Text countText;
    private bool isSelected = false;

    private Image itemImg;
    private GameObject selectImg;
    private InventoryUI inventory;
    private int index;

    private void Awake()
    {
        button = GetComponentInChildren<Button>();
        itemImg = transform.GetChild(1).GetComponent<Image>();
        selectImg = transform.GetChild(0).gameObject;
        inventory = GetComponentInParent<InventoryUI>();
    }

    private void Start()
    {
        button.onClick.AddListener(OnClickButton);
    }

    public void SetItem(Item item, bool check = false)
    {
        this.item = item;
        itemImg.sprite = ItemManager.Instance.GetItemSprite(item);
        transform.GetChild(2).gameObject.SetActive(check);
    }

    public void ResetItem()
    {
        item = null;
        transform.GetChild(1).GetComponent<Image>().sprite = null;
        OffSelect();
        CheckOff();
    }

    public Item GetItem()
    {
        return item;
    }

    private void OnClickButton()
    {
        if (object.ReferenceEquals(item, null)) return;
        isSelected = !isSelected;
        if(isSelected)
        {
            itemText.text = item.itemDesc;
            countText.text = SaveManager.Instance.itemMap[item].ToString();
            inventory.ChangeSelectedSlot(index);
        }
        else
        {
            itemText.text = "아이템 설명";
            countText.text = "0";
            inventory.ChangeSelectedSlot(-1);
        }
        if(inventory.IsSelectMode) 
        {
            inventory.OnClickSelect(index);
        }
        selectImg.SetActive(isSelected);
    }

    public void OffSelect()
    {
        isSelected = false;
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public bool CheckOn()
    {
        bool isActive = transform.GetChild(2).gameObject.activeSelf;
        transform.GetChild(2).gameObject.SetActive(!isActive);
        return !isActive;
    }

    public void CheckOff() 
    {
        transform.GetChild(2).gameObject.SetActive(false);
    }

    public void SetItemText(ref Text itemText, ref Text countText, int index) 
    {
        this.itemText = itemText;
        this.countText = countText;
        this.index = index;
    }
}
 