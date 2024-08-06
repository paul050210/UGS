using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class DictionarySlotUI : MonoBehaviour
{
    [SerializeField] private Sprite defaultImage;
    [SerializeField] private Image thisImage;
    [SerializeField] private Button thisButton;
    [SerializeField] private Image itemImage;
    [SerializeField] private Text nameText;
    [SerializeField] private Text descText;
    private KeyValuePair<Item, bool> slotInfo;


    private void Start()
    {
        thisButton.onClick.AddListener(OnClickButton);
    }

    public void SetSlot(KeyValuePair<Item, bool> pair)
    {
        slotInfo = pair;
        if(slotInfo.Value)
        {
            thisImage.sprite = ItemManager.Instance.GetItemSprite(slotInfo.Key);
        }
        else
        {
            thisImage.sprite = defaultImage;
        }
        
    }

    private void OnClickButton()
    {
        if(slotInfo.Value)
        {
            itemImage.sprite = ItemManager.Instance.GetItemSprite(slotInfo.Key);
            nameText.text = slotInfo.Key.itemName;
            descText.text = slotInfo.Key.itemDesc;
        }
        else
        {
            // ?이미지 같은거 추가
            itemImage.sprite = null;
            nameText.text = "미획득 아이템";
            descText.text = "아직 획득하지 못한 아이템 입니다";
        }
    }
}
