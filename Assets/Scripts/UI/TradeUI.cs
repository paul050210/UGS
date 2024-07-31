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

    // ���� ���õ� �������� �˰� �־�ߵ�
    // �׺��Ѽ� ������ �������ϸ� �ŷ��� ����Ǿߵ�
}
