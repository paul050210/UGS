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
    [SerializeField] private List<ItemSlotUI> slots;
    [SerializeField] private Text itemText;
    [SerializeField] private Text countText;
    [SerializeField] private Button selectButton;
    [SerializeField] private Button[] stateButtons;
    
    private int selectedSlot = -1;

    private InvenState state = InvenState.all;

    private void Start()
    {
        SaveManager.Instance.LoadItemData();
        SetItemSlot();
        SetItemButton();
        SetStateButton();
        selectButton.onClick.AddListener(OnClickSelect);
    }

    private void OnEnable()
    {
        state = InvenState.all;
        ResetItemSlot();
        SetItemSlot();
        itemText.text = "아이템설명";
        countText.text = "0";
    }

    private void SetItemButton()
    {
        for(int i = 0; i<slots.Count; i++) 
        {
            slots[i].SetItemText(ref itemText, ref countText, i);
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

            
            slots[i].SetItem(p.Key);
            i++;
            
        }
    }

    private void ResetItemSlot()
    {
        selectedSlot = -1;
        for (int i = 0; i<slots.Count; i++)
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
                ResetItemSlot();
                SetItemSlot();
                itemText.text = "아이템설명";
                countText.text = "0";
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

    private void OnClickSelect()
    {
        if (selectedSlot == -1) return;
        slots[selectedSlot].CheckOn();
    }
}
 