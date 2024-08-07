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
    [SerializeField] private RectTransform contents;
    [SerializeField] private Image[] productImages;
    [SerializeField] private GameObject[] plusTexts;
    [SerializeField] private CombiUI[] combis;
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
        for (int i = 0; i < productImages.Length; i++)
        {
            productImages[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < plusTexts.Length; i++)
        {
            plusTexts[i].SetActive(false);
        }
        for(int i = 0; i<combis.Length; i++)
        {
            combis[i].RestCombi();
        }
        if (slotInfo.Key == null) return;
        
        contents.anchoredPosition = new Vector2(0f, 0f);
        if (slotInfo.Value)
        {
            contents.sizeDelta = new Vector2(0f, 950f);
            itemImage.sprite = ItemManager.Instance.GetItemSprite(slotInfo.Key);
            nameText.text = slotInfo.Key.itemName;
            descText.text = slotInfo.Key.itemDesc;
            var items = ItemManager.Instance.GetDicData(slotInfo.Key);
            if (items == null) return;

            for(int i = 0; i<items.Count; i++)
            {
                productImages[i].gameObject.SetActive(true);
                if (SaveManager.Instance.itemDicMap[items[i].item])
                {
                    productImages[i].sprite = items[i].sprite;
                }
                else
                {
                    productImages[i].sprite = defaultImage;
                }
                if(i < items.Count-1)
                {
                    plusTexts[i].SetActive(true);
                }
            }
            
            var recipes = ItemManager.Instance.GetDicData2(slotInfo.Key);
            if (recipes == null) return;
            contents.sizeDelta = new Vector2(0f, (recipes.Count - 1) * 100f + 950f);
            for (int i = 0; i<recipes.Count; i++)
            {
                combis[i].SetCombi(recipes[i].BaseItems, recipes[i].MergeItem);
            }
        }
        else
        {
            // ?이미지 같은거 추가
            itemImage.sprite = null;
            nameText.text = "미획득 아이템";
            descText.text = "아직 획득하지 못한 아이템 입니다";
            contents.sizeDelta = new Vector2(0f, 700f);
        }
    }
}
