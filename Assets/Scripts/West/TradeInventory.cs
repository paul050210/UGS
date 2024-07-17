using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TradeInventoryUI : MonoBehaviour
{
    [SerializeField] private Text itemText;
    [SerializeField] private Button handOverButton;
    [SerializeField] private Button[] stateButtons;

    private ItemSlotUI[] slots = null;
    private List<UIItem> selectedItems = new List<UIItem>();
    private InvenState state = InvenState.all;

    private Dictionary<UIItem, int> itemMap = new Dictionary<UIItem, int>(); // ������ �����͸� ������ ��

    private void Start()
    {
        slots = transform.GetChild(0).GetComponentsInChildren<ItemSlotUI>();
        SetItemButton();
        SetStateButton();
        handOverButton.onClick.AddListener(HandOverItems);
        UpdateUI();
    }

    private void OnEnable()
    {
        state = InvenState.all;
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
                UpdateUI();
                itemText.text = "������ ����";
            });
        }
    }

    private void HandOverItems()
    {
        int totalValue = CalculateTotalValue();
        // TradeGuideUI�� totalValue�� �����ϴ� ���� �߰�
        // �º� ���� ���� �߰�
        // ������ ������ ���� ����

        // ���÷� TradeGuideUI�� �����ϴ� ���
        TradeGuideUI.Instance.SetTotalValue(totalValue);
        gameObject.SetActive(false);
    }

    private int CalculateTotalValue()
    {
        int totalValue = 0;
        foreach (var item in selectedItems)
        {
            totalValue += item.item.value; // �� �������� ����ġ�� �ջ�
        }
        handOverButton.interactable = totalValue > 0;
        return totalValue;
    }

    public void UpdateTradeUI(Dictionary<UIItem, int> updatedItemMap)
    {
        itemMap = updatedItemMap;
        UpdateUI();
    }

    private void UpdateUI()
    {
        ResetItemSlot();
        SetItemSlot();
    }

    public void OnTradeCompleted()
    {
        selectedItems.Clear();
        UpdateUI();
    }
}
