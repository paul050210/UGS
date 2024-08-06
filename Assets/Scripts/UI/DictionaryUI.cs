using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DictionaryUI : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private Text nameText;
    [SerializeField] private Text descText;


    private DictionarySlotUI[] slots;
    private List<KeyValuePair<Item, bool>> pageItemList = new List<KeyValuePair<Item, bool>>();
    private int curPage;
    private const int onePageSlot = 10;



    private void OnEnable()
    {
        itemImage.sprite = null;
        nameText.text = "이름";
        descText.text = "설명";
        curPage = 1;
        SetSlots();
        // 아이템 슬롯들 초기화
        // 아래 조합법등 초기화
        // 오른쪽 이미지, 텍스트 초기화
    }

    private void SetSlots()
    {
        if(slots == null)
        {
            slots = GetComponentsInChildren<DictionarySlotUI>();
        }
        pageItemList?.Clear();
        foreach(var pair in SaveManager.Instance.itemDicMap)
        {
            pageItemList.Add(pair);
        }
        int start = (curPage - 1) * onePageSlot;
        for(int i = 0; i<onePageSlot;i++)
        {
            slots[i].SetSlot(pageItemList[start + i]);
        }
    }


}
