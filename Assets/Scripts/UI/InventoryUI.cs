using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// InvenState 열거형 추가
public enum InvenState
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
    [SerializeField] private TradeInventoryUI tradeInventoryUI; // TradeInventoryUI 참조 추가

    private ItemSlotUI[] slots = null;
    private int selectedSlot = -1;
    private int maxSelected = 5;
    private bool isSelecteMode = false;
    public bool IsSelectMode => isSelecteMode;
    private List<UIItem> selectedItems = new List<UIItem>();

    private InvenState state = InvenState.all;
    private ItemMerge itemMerge = null;

    private Dictionary<UIItem, int> itemMap = new Dictionary<UIItem, int>(); // 아이템 데이터를 저장할 맵

    private void Start()
    {
        LoadItemData(); // 데이터 로드
        slots = transform.GetChild(0).GetComponentsInChildren<ItemSlotUI>();
        itemMerge = GetComponent<ItemMerge>();
        SetItemButton();
        SetStateButton();
        selectButton.onClick.AddListener(SelectButton);
        UpdateUI();
    }

    private void OnEnable()
    {
        state = InvenState.all;
        selectedItems.Clear();
        isSelecteMode = false;
        selectedSlot = -1;
        UpdateUI();
    }

    private void SetItemButton()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].SetItemText(ref itemText, i);
        }
    }

    private void SetItemSlot()
    {
        int i = 0;
        foreach (var p in itemMap)
        {
            if (p.Value == 0)
                continue;

            switch (state)
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

            for (int j = 0; j < p.Value; j++)
            {
                var item = new UIItem(p.Key, j);
                slots[i].SetItem(item, selectedItems.Contains(item));
                if (i == selectedSlot)
                    slots[i].OnSelect();
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
        for (int i = 0; i < stateButtons.Length; i++)
        {
            int temp = i;
            stateButtons[temp].onClick.AddListener(() =>
            {
                state = (InvenState)temp;
                selectedSlot = -1;
                UpdateUI();
                itemText.text = "아이템 설명";
            });
        }
    }

    public void ChangeSelectedSlot(int index)
    {
        if (selectedSlot != -1)
        {
            slots[selectedSlot].OffSelect();
        }
        selectedSlot = index;
    }

    public void OnClickSelect(int index)
    {
        if (slots[index].CheckOn())
        {
            selectedItems.Add(slots[index].GetItem());
            if (selectedItems.Count > maxSelected)
            {
                selectedItems.RemoveAt(0);
                UpdateUI();
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
        itemText.text = "아이템 설명";
    }

    private void UpdateUI()
    {
        ResetItemSlot();
        SetItemSlot();
    }

    public void LoadItemData()
    {
        // Load item data into itemMap
    }

    public void AddItem(UIItem item, int count)
    {
        if (itemMap.ContainsKey(item))
        {
            itemMap[item] += count;
        }
        else
        {
            itemMap[item] = count;
        }
        UpdateUI();
        tradeInventoryUI?.UpdateTradeUI(itemMap); // TradeInventoryUI에 업데이트 알림
    }

    public void RemoveItem(UIItem item, int count)
    {
        if (itemMap.ContainsKey(item))
        {
            itemMap[item] -= count;
            if (itemMap[item] <= 0)
            {
                itemMap.Remove(item);
            }
            UpdateUI();
            tradeInventoryUI?.UpdateTradeUI(itemMap); // TradeInventoryUI에 업데이트 알림
        }
    }

    public Dictionary<UIItem, int> GetItemMap()
    {
        return itemMap;
    }
}
