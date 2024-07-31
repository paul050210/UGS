using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeUI : MonoBehaviour
{
    [SerializeField] private Text nameText;
    [SerializeField] private Image charImage;
    [SerializeField] private List<TradeItemSlotUI> slots;
    
    private int selectedSlot = -1;
    private Item selectedItem;

    public void TurnOn(TradeInfo tInfo)
    {
        gameObject.SetActive(true);
        nameText.text = tInfo.traderName;
        charImage.sprite = tInfo.charSprite;
        selectedSlot = -1;
        // TODO: 거래 입장대화
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

    // 현재 선택된 아이템을 알고 있어야됨
    // 테블릿켜서 아이템 선택을하면 거래가 진행되야됨
}
