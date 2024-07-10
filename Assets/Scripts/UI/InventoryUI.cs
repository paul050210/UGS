using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


enum InvenState
{
    all,
    herb,
    stone,
    bio,
    potion,
    another
}
public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Text itemText;
    [SerializeField] private Button selectButton;
    [SerializeField] private Button[] stateButtons;
    private ItemSlotUI[] slots = null;
        
    private int selectedSlot = -1;
    private int maxSelected = 5;
    private bool isSelecteMode = false;
    public bool IsSelectMode => isSelecteMode;
    private List<UIItem> selectedItems = new List<UIItem>();

    private InvenState state = InvenState.all;

    private void Start()
    {
        SaveManager.Instance.LoadItemData();
        slots = transform.GetChild(0).GetComponentsInChildren<ItemSlotUI>();
        SetItemSlot();
        SetItemButton();
        SetStateButton();
        selectButton.onClick.AddListener(SelectButton);
    }

    private void OnEnable()
    {
        state = InvenState.all;
        selectedItems.Clear();
        isSelecteMode = false;
        selectedSlot = -1;
        ResetItemSlot();
        SetItemSlot();
        itemText.text = "아이템설명";
    }

    private void SetItemButton()
    {
        for(int i = 0; i<slots.Length; i++) 
        {
            slots[i].SetItemText(ref itemText, i);
        }
    }

    private void SetItemSlot()
    {
        int i = 0;
        foreach (var p in SaveManager.Instance.itemMap)
        {
            if (p.Value == 0)
                continue;
            switch(state)
            {
                case InvenState.herb:
                    if (p.Key.type != ItemType.herb)
                        continue;
                    break;
                case InvenState.stone:
                    if (p.Key.type != ItemType.stone)
                        continue;
                    break;
                case InvenState.bio:
                    if (p.Key.type != ItemType.bio)
                        continue;
                    break;
                case InvenState.potion:
                    if (p.Key.type != ItemType.potion)
                        continue;
                    break;
                case InvenState.another:
                    if (p.Key.type != ItemType.another)
                        continue;
                    break;
                default: 
                    break;
            }

            for(int j = 0; j < p.Value; j++)
            {
                var item = new UIItem(p.Key, j);
                slots[i].SetItem(item, selectedItems.Contains(item));
                i++;
            }
            
        }
    }

    private void ResetItemSlot()
    {
        if (slots == null) return;
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].ResetItem();
        }
    }

    private void SetStateButton()
    {
        for(int i = 0; i < stateButtons.Length; i++) 
        {
            int temp = i;
            stateButtons[temp].onClick.AddListener(() => 
            {
                state = (InvenState)temp;
                selectedSlot = -1;
                ResetItemSlot();
                SetItemSlot();
                itemText.text = "아이템설명";
            });
        }
    }

    public void ChangeSelectedSlot(int index)
    {
        if(selectedSlot != -1)
        {
            slots[selectedSlot].OffSelect();
        }
        selectedSlot = index;
    }

    public void OnClickSelect(int index)
    {
        if(slots[index].CheckOn())
        {
            selectedItems.Add(slots[index].GetItem());
            if (selectedItems.Count > maxSelected)
            {
                selectedItems.RemoveAt(0);
                ResetItemSlot();
                SetItemSlot();
            }
        }
        else
        {
            selectedItems.Remove(slots[index].GetItem());
        }
    }

    private void SelectButton()
    {
        isSelecteMode = !isSelecteMode;
        ChangeSelectedSlot(-1);
        itemText.text = "아이템설명";
    }

    public Item[] GetToMerge()
    {
        if(selectedItems.Count < 2)
        {
            Debug.LogWarning("선택된 아이템 부족");
            return null;
        }
        Item[] items = new Item[selectedItems.Count];
        for(int i = 0; i<selectedItems.Count; i++) 
        {
            items[i] = selectedItems[i].item;
        }

        return items;
    }
}
 