using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeItemSlotUI : MonoBehaviour
{
    [SerializeField] private Text descText;
    [SerializeField] private Image slotImage;
    [SerializeField] private Button button;
    
    private Item slotItem;
    private TradeUI tradeUI;
    private string slotDesc;
    private int index;
    private bool isSelected = false;

    private void Awake()
    {
        tradeUI = GetComponentInParent<TradeUI>();
    }

    private void Start()
    {
        button.onClick.AddListener(OnClickButton);
    }


    public void SetSlot(Item item, string text, int index)
    {
        slotItem = item;
        slotDesc = text;
        this.index = index;
        slotImage.sprite = ItemManager.Instance.GetItemSprite(item);
        isSelected = false;
        transform.GetChild(1).gameObject.SetActive(false);
    }

    private void OnClickButton()
    {
        if (slotItem.Equals(null)) return;
        isSelected = !isSelected;
        if(isSelected)
        {
            descText.text = slotDesc;
            tradeUI.ChangeSelectedSlot(index, slotItem);
        }
        else
        {
            descText.text = "ºóÄ­Ã¤¿ì±â¿ë";
            tradeUI.ChangeSelectedSlot(-1);
        }
        transform.GetChild(1).gameObject.SetActive(isSelected);
        
    }

    public void OffSelect()
    {
        isSelected = false;
        transform.GetChild(1).gameObject.SetActive(false);
        
    }
}
