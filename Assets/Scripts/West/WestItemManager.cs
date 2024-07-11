using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WestItemManager : MonoBehaviour
{
    public static WestItemManager Instance;

    public List<UIItem> items; // 모든 아이템 리스트
    public Button handOverButton; // 건네기 버튼
    public Text infoText; // 정보 출력 텍스트

    private List<UIItem> selectedItems = new List<UIItem>();
    private int purchasePrice = 0;
    private int sellingPrice = 0;
    private int traderPreference = 10; // 예시로 설정한 거래자의 선호도
    private int traderFavor = 5; // 예시로 설정한 거래자와의 호감도

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        handOverButton.onClick.AddListener(OnHandOverButtonClicked);
        handOverButton.gameObject.SetActive(false);
        UpdateInfoText();
    }

    public void AddSelectedItem(UIItem item)
    {
        selectedItems.Add(item);
        purchasePrice += item.item.baseValue + item.item.playerPreference;
        handOverButton.gameObject.SetActive(true);
        UpdateInfoText();
    }

    public void RemoveSelectedItem(UIItem item)
    {
        selectedItems.Remove(item);
        purchasePrice -= item.item.baseValue + item.item.playerPreference;
        handOverButton.gameObject.SetActive(selectedItems.Count > 0);
        UpdateInfoText();
    }

    private void OnHandOverButtonClicked()
    {
        sellingPrice = 0;

        foreach (var item in selectedItems)
        {
            sellingPrice += item.item.baseValue + traderPreference;
        }

        sellingPrice -= traderFavor;

        if (sellingPrice < purchasePrice)
        {
            ShowMessage("교환 불가");
        }
        else
        {
            ShowMessage("교환 성공");
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

        UpdateInfoText();
    }

    private void UpdateInfoText()
    {
        infoText.text = $"구매가: {purchasePrice}\n판매가: {sellingPrice}";
    }

    private void ShowMessage(string message)
    {
        StartCoroutine(DisplayText(message));
    }

    private IEnumerator DisplayText(string message)
    {
        infoText.text = "";
        foreach (char letter in message.ToCharArray())
        {
            infoText.text += letter;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
