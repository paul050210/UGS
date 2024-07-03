using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    private Item item;
    private Button button;
    private Text itemText;
    private Text countText;

    private void Start()
    {
        button = GetComponentInChildren<Button>();
        button.onClick.AddListener(OnClickButton);
    }

    public void SetItem(Item item)
    {
        this.item = item;
        GetComponentInChildren<Image>().sprite = ItemManager.Instance.GetItemSprite(item);
    }

    public void ResetItem()
    {
        item = null;
        GetComponentInChildren<Image>().sprite = null;
    }

    private void OnClickButton()
    {
        if (object.ReferenceEquals(item, null)) return;
        itemText.text = item.itemDesc;
        countText.text = SaveManager.Instance.itemMap[item].ToString();
    }

    public void SetItemText(ref Text itemText, ref Text countText) 
    {
        this.itemText = itemText;
        this.countText = countText;
    }
}
