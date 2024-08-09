using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MapManager : MonoBehaviour
{
    public GameObject mapPanel;
    public GameObject[] tradePanels;  // 거래 화면 패널 배열
    public Button[] locationButtons;  // 투명 버튼 배열
    public GameObject black;  // 블랙아웃 
    public Button[] tradeCloseButtons;  // 거래 화면 종료 버튼 배열
    private int currentTradeIndex = -1;


    void Start()
    {
        // 위치 버튼 설정
        for (int i = 0; i < locationButtons.Length; i++)
        {
            int index = i;  // 로컬 복사본을 사용하여 클로저 문제를 해결합니다.
            locationButtons[i].onClick.AddListener(() => OnLocationButtonClick(index));
        }

        // 거래 패널 종료 버튼 설정
        for (int i = 0; i < tradeCloseButtons.Length; i++)
        {
            tradeCloseButtons[i].onClick.AddListener(CloseTradePanel);
        }

        // 초기화
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

        // 다른 버튼 및 맵 패널 비활성화
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

            // 맵 패널 활성화 및 버튼 활성화
            mapPanel.SetActive(true);
            foreach (var button in locationButtons)
            {
                button.interactable = true;
            }
        }
    }
}
