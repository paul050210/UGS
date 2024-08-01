using System;
using System.Collections;
using System.Collections.Generic;
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
        nameText.text = tInfo.traderName;
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
        tabletUI.TurnOnTablet(State.Inventory);
        // �����۰�ġ+ȣ������ ��ġ ��
        if(true)
        {
            descText.text = "�ŷ� ����";
            int n = ItemManager.Instance.GetItem(selectedItem);
            ItemManager.Instance.AddItem(selectedItem, n+1);
        }
        else
        {
            descText.text = "�ŷ� ����";
        }
    }
}
