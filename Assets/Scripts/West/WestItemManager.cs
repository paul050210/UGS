using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WestItemManager : MonoBehaviour
{
    public static WestItemManager Instance;

    public List<UIItem> items; // ��� ������ ����Ʈ
    public Button handOverButton; // �ǳױ� ��ư
    public Text infoText; // ���� ��� �ؽ�Ʈ

    private List<UIItem> selectedItems = new List<UIItem>();
    private int purchasePrice = 0;
    private int sellingPrice = 0;
    private int traderPreference = 10; // ���÷� ������ �ŷ����� ��ȣ��
    private int traderFavor = 5; // ���÷� ������ �ŷ��ڿ��� ȣ����

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
            ShowMessage("��ȯ �Ұ�");
        }
        else
        {
            ShowMessage("��ȯ ����");
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

        UpdateInfoText();
    }

    private void UpdateInfoText()
    {
        infoText.text = $"���Ű�: {purchasePrice}\n�ǸŰ�: {sellingPrice}";
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
