using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TradeUI : MonoBehaviour
{
    [SerializeField] private Text nameText;
    [SerializeField] private Text descText;
    [SerializeField] private Image charImage;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button doneButton;
    [SerializeField] private List<TradeItemSlotUI> slots;
    [SerializeField] private InventoryUI inventoryUI;

    private TradeInfo tradeInfo;
    private int selectedSlot = -1;
    private Item selectedItem;
    private TabletUI tabletUI;
   

    private void Start()
    {
        closeButton.onClick.AddListener(CloseTrade);
    }

    public void TurnOn(TradeInfo tInfo)
    {
        if(tabletUI == null)
        {
            tabletUI = FindObjectOfType<TabletUI>();
        }
        gameObject.SetActive(true);
        tradeInfo = tInfo;
        nameText.text = tInfo.traderName;
        descText.text = "��ĭä����";
        charImage.sprite = tInfo.charSprite;
        selectedSlot = -1;
        doneButton.onClick.AddListener(Trade);
        // TODO: �ŷ� �����ȭ
        for(int i = 0; i<slots.Count; i++)
        {
            try
            {
                slots[i].SetSlot(tInfo.itemList[i], tInfo.textList[i], i);
            }
            catch (Exception e)
            {
                slots[i].SetSlot(null, null, -1);
            }
        }
    }

    public void ChangeSelectedSlot(int index, Item item = null)
    {
        if(selectedSlot != -1)
        {
            slots[selectedSlot].OffSelect();
        }
        selectedSlot = index;
        selectedItem = item;
    }

    private void CloseTrade()
    {
        gameObject.SetActive(false);
        doneButton.onClick.RemoveListener(Trade);
    }

    private void Trade()
    {
        if(selectedItem == null)
        {
            // TODO: ����׿��� �ٸ� ���𰥷� �ٲٱ�
            Debug.LogWarning("�ŷ��� �������� �����ϼ���.");
            return;
        }
        Item myItem = inventoryUI.GetToDecom();
        if (myItem == null) return;

        slots[selectedSlot].OffSelect();
        selectedSlot = -1;
        tabletUI.TurnOnTablet(State.Inventory);
        // �����۰�ġ+ȣ������ ��ġ ��
        if(myItem.itemPrice >= selectedItem.itemPrice)
        {
            descText.text = "�ŷ� ����";
            int n = ItemManager.Instance.GetItem(selectedItem);
            ItemManager.Instance.AddItem(selectedItem, n+1);
            n = ItemManager.Instance.GetItem(myItem);
            ItemManager.Instance.AddItem(myItem, n - 1);
        }
        else
        {
            descText.text = "�ŷ� ����";
        }
        selectedItem = null;
    }
}
