using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField] private Sprite defaultImage;

    private UIItem item;
    private Button button;
    private Text itemText;
    private Text nameText;
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

    public void SetItem(UIItem item, bool check = false)
    {
        this.item = item;
        if(itemImg == null)
        {
            Awake();
        }
        itemImg.sprite = ItemManager.Instance.GetItemSprite(item.item);
        transform.GetChild(2).gameObject.SetActive(check);
    }

    public void ResetItem()
    {
        item = null;
        transform.GetChild(1).GetComponent<Image>().sprite = defaultImage;
        OffSelect();
        CheckOff();
    }

    public UIItem GetItem()
    {
        return item;
    }

    private void OnClickButton()
    {
        if (object.ReferenceEquals(item, null)) return;
        isSelected = !isSelected;
        if(isSelected)
        {
            nameText.text = item.item.itemName;
            itemText.text = item.item.itemDesc;
            inventory.ChangeSelectedSlot(index);
        }
        else
        {
            nameText.text = "아이템 이름";
            itemText.text = "아이템 설명";
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

    public void OnSelect()
    {
        isSelected = true;
        transform.GetChild(0).gameObject.SetActive(true);
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

    public void SetItemText(ref Text NameText, ref Text itemText, int index) 
    {
        this.nameText = NameText;
        this.itemText = itemText;
        this.index = index;
    }
}
 