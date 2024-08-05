using DG.Tweening;
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
        descText.text = "빈칸채우기용";
        charImage.sprite = tInfo.charSprite;
        selectedSlot = -1;
        doneButton.onClick.AddListener(Trade);
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

    private void CloseTrade()
    {
        gameObject.SetActive(false);
        ItemManager.Instance.canSelect = false;
        Camera.main.DOFieldOfView(60f, 0.3f).SetEase(Ease.Linear);
        Camera.main.gameObject.transform.DOMove(new Vector3(0f, 1f, 0f), 0.3f).SetEase(Ease.Linear);
        doneButton.onClick.RemoveListener(Trade);
    }

    private void Trade()
    {
        if(selectedItem == null)
        {
            // TODO: 디버그에서 다른 무언갈로 바꾸기
            Debug.LogWarning("거래할 아이템을 선택하세요.");
            return;
        }
        Item[] myItems = inventoryUI.GetToMerge(1);
        if (myItems == null) return;

        slots[selectedSlot].OffSelect();
        selectedSlot = -1;
        tabletUI.TurnOnTablet(State.Inventory);
        int myPrice = 0;
        for(int i = 0; i<myItems.Length; i++)
        {
            myPrice += myItems[i].itemPrice;
        }
        // 아이템가치+호감도로 수치 비교
        if(myPrice >= selectedItem.itemPrice)
        {
            descText.text = "거래 성공";
            int n = ItemManager.Instance.GetItem(selectedItem);
            ItemManager.Instance.AddItem(selectedItem, n+1);
            for (int i = 0; i < myItems.Length; i++)
            {
                n = ItemManager.Instance.GetItem(myItems[i]);
                ItemManager.Instance.AddItem(myItems[i], n - 1);
            }
        }
        else
        {
            descText.text = "거래 실패";
        }
        selectedItem = null;
    }
}
