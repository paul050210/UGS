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
        nameText.text = "�̸�";
        descText.text = "����";
        curPage = 1;
        SetSlots();
        // ������ ���Ե� �ʱ�ȭ
        // �Ʒ� ���չ��� �ʱ�ȭ
        // ������ �̹���, �ؽ�Ʈ �ʱ�ȭ
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
