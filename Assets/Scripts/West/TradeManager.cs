using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TradeManager : MonoBehaviour
{
    public GameObject tradeGuidePanel;
    public Button tradeButton;
    public TradeGuideUI tradeGuideUI;
    public ItemShelfUI itemShelfUI;
    public CollectionManager collectionManager;

    private List<UIItem> selectedItems;

    void Start()
    {
        tradeGuidePanel.SetActive(false);
        selectedItems = new List<UIItem>();
        tradeButton.onClick.AddListener(OnTradeButtonClicked);
    }

    public void SelectItem(UIItem item)
    {
        if (!selectedItems.Contains(item))
        {
            selectedItems.Add(item);
        }
    }

    public void DeselectItem(UIItem item)
    {
        if (selectedItems.Contains(item))
        {
            selectedItems.Remove(item);
        }
    }

    void OnTradeButtonClicked()
    {
        // Check if trade conditions are met
        bool tradeSuccess = CheckTradeConditions();

        if (tradeSuccess)
        {
            // Remove selected items from collection
            foreach (UIItem item in selectedItems)
            {
                collectionManager.RemoveItem(item);
            }

            // Add new items to the shelf
            foreach (UIItem item in selectedItems)
            {
                itemShelfUI.AddItem(item);
            }

            // Display trade success dialogue
            tradeGuideUI.StartDialogue(new List<string> { "거래가 완료되었습니다!" });
        }
        else
        {
            // Display trade failure dialogue
            tradeGuideUI.StartDialogue(new List<string> { "거래 조건을 충족하지 못했습니다." });
        }

        // Clear selected items
        selectedItems.Clear();
        tradeGuidePanel.SetActive(true);
    }

    bool CheckTradeConditions()
    {
        // Implement trade conditions check logic
        // For now, return true for successful trade
        return true;
    }
}
