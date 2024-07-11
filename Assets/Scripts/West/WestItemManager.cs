using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WestItemManager : MonoBehaviour
{
    public List<WestItem> items; // ������ ����Ʈ
    public Button handOverButton; // �ǳױ� ��ư
    public Text infoText; // ���� ��� �ؽ�Ʈ

    private List<WestItem> selectedItems = new List<WestItem>();
    private int purchasePrice = 0;
    private int sellingPrice = 0;
    private int traderPreference = 10; // ���÷� ������ �ŷ����� ��ȣ��
    private int traderFavor = 5; // ���÷� ������ �ŷ��ڿ��� ȣ����

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
            item.gameObject.GetComponent<Image>().color = Color.white; // ���� �������� ����
        }
        else
        {
            selectedItems.Add(item);
            purchasePrice += item.GetItemValue();
            item.gameObject.GetComponent<Image>().color = Color.green; // ���õ� �������� ����
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
            infoText.text = "��ȯ �Ұ�";
        }
        else
        {
            infoText.text = "��ȯ ����";
            foreach (var item in selectedItems)
            {
                // ������ ��ȯ ó��
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
        infoText.text = $"���Ű�: {purchasePrice}\n�ǸŰ�: {sellingPrice}";
    }
}
