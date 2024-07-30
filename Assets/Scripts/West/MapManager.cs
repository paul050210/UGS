using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MapManager : MonoBehaviour
{
    public GameObject mapPanel;
    public GameObject[] tradePanels;  // �ŷ� ȭ�� �г� �迭
    public Button[] locationButtons;  // ���� ��ư �迭
    public GameObject black;  // ���ƿ� 
    public Button[] tradeCloseButtons;  // �ŷ� ȭ�� ���� ��ư �迭
    private int currentTradeIndex = -1;


    void Start()
    {
        // ��ġ ��ư ����
        for (int i = 0; i < locationButtons.Length; i++)
        {
            int index = i;  // ���� ���纻�� ����Ͽ� Ŭ���� ������ �ذ��մϴ�.
            locationButtons[i].onClick.AddListener(() => OnLocationButtonClick(index));
        }

        // �ŷ� �г� ���� ��ư ����
        for (int i = 0; i < tradeCloseButtons.Length; i++)
        {
            tradeCloseButtons[i].onClick.AddListener(CloseTradePanel);
        }

        // �ʱ�ȭ
        mapPanel.SetActive(true);
        foreach (var panel in tradePanels)
        {
            panel.SetActive(false);
        }
        black.SetActive(false);
    }

    void OnLocationButtonClick(int index)
    {
        Debug.Log("Location button clicked, index: " + index);
        StartCoroutine(ShowTradePanel(index));
    }

    IEnumerator ShowTradePanel(int index)
    {
        black.SetActive(true);
        yield return new WaitForSeconds(0.2f);

        foreach (var panel in tradePanels)
        {
            panel.SetActive(false);
        }

        tradePanels[index].SetActive(true);
        black.SetActive(false);
        currentTradeIndex = index;

        // �ٸ� ��ư �� �� �г� ��Ȱ��ȭ
        mapPanel.SetActive(false);
        foreach (var button in locationButtons)
        {
            button.interactable = false;
        }

    }



    void CloseTradePanel()
    {
        if (currentTradeIndex != -1)
        {
            tradePanels[currentTradeIndex].SetActive(false);
            currentTradeIndex = -1;

            // �� �г� Ȱ��ȭ �� ��ư Ȱ��ȭ
            mapPanel.SetActive(true);
            foreach (var button in locationButtons)
            {
                button.interactable = true;
            }
        }
    }
}
