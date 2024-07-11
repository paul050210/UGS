using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WestItemManager : MonoBehaviour
{
    public List<WestItem> items; // 아이템 리스트
    public Button handOverButton; // 건네기 버튼
    public Text infoText; // 정보 출력 텍스트

    private List<WestItem> selectedItems = new List<WestItem>();
    private int purchasePrice = 0;
    private int sellingPrice = 0;
    private int traderPreference = 10; // 예시로 설정한 거래자의 선호도
    private int traderFavor = 5; // 예시로 설정한 거래자와의 호감도

    void Start()
    {
        handOverButton.onClick.AddListener(OnHandOverButtonClicked);
        handOverButton.gameObject.SetActive(false);
        UpdateInfoText();
    }

    public void OnItemClick(WestItem item)
    {
        if (selectedItems.Contains(item))
        {
            selectedItems.Remove(item);
            purchasePrice -= item.GetItemValue();
            item.gameObject.GetComponent<Image>().color = Color.white; // 원본 색상으로 변경
        }
        else
        {
            selectedItems.Add(item);
            purchasePrice += item.GetItemValue();
            item.gameObject.GetComponent<Image>().color = Color.green; // 선택됨 색상으로 변경
        }

        handOverButton.gameObject.SetActive(purchasePrice > 0);
        UpdateInfoText();
    }

    void OnHandOverButtonClicked()
    {
        foreach (var item in selectedItems)
        {
            sellingPrice += item.baseValue + traderPreference;
        }

        sellingPrice -= traderFavor;

        if (sellingPrice < purchasePrice)
        {
            infoText.text = "교환 불가";
        }
        else
        {
            infoText.text = "교환 성공";
            foreach (var item in selectedItems)
            {
                // 아이템 교환 처리
                item.gameObject.SetActive(false);
            }
            selectedItems.Clear();
            purchasePrice = 0;
            sellingPrice = 0;
            handOverButton.gameObject.SetActive(false);
        }
    }

    void UpdateInfoText()
    {
        infoText.text = $"구매가: {purchasePrice}\n판매가: {sellingPrice}";
    }
}
