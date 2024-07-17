using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShelfUI : MonoBehaviour
{
    [SerializeField] private Transform itemSlotContainer;
    [SerializeField] private GameObject itemSlotPrefab;

    private List<UIItem> shelfItems;

    void Start()
    {
        shelfItems = new List<UIItem>();
        RefreshShelf();
    }

    public void AddItem(UIItem item)
    {
        shelfItems.Add(item);
        RefreshShelf();
    }

    public void RemoveItem(UIItem item)
    {
        shelfItems.Remove(item);
        RefreshShelf();
    }

    public void RefreshShelf()
    {
        foreach (Transform child in itemSlotContainer)
        {
            Destroy(child.gameObject);
        }
        foreach (UIItem item in shelfItems)
        {
            GameObject itemSlotObject = Instantiate(itemSlotPrefab, itemSlotContainer);
            ItemSlotUI itemSlotUI = itemSlotObject.GetComponent<ItemSlotUI>();
            itemSlotUI.SetItem(item);
        }
    }
}
