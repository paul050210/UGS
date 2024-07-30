using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public GameObject mapPanel; public GameObject collectionPanel; public Button mapButton; public Button collectionButton; public Button mapCloseButton; public Button collectionCloseButton; public CollectionManager collectionManager;

    void Start()
    {
        mapPanel.SetActive(false); collectionPanel.SetActive(false);
        mapButton.onClick.AddListener(ShowMapPanel); collectionButton.onClick.AddListener(ShowCollectionPanel); mapCloseButton.onClick.AddListener(CloseMapPanel); collectionCloseButton.onClick.AddListener(CloseCollectionPanel);
    }

    void ShowMapPanel()

    {
        if (!collectionPanel.activeSelf)
        {
            mapPanel.SetActive(true); collectionPanel.SetActive(false); collectionButton.interactable = false;  // �÷��� ��ư ��Ȱ��ȭ
        }
    }

    void ShowCollectionPanel()
    {
        if (!mapPanel.activeSelf)  // �� �г��� ��Ȱ��ȭ ������ ���� �÷��� �г� Ȱ��ȭ
        { mapPanel.SetActive(false); collectionPanel.SetActive(true); mapButton.interactable = false; collectionManager.ShowPage(0); }
    }

    void CloseMapPanel() { mapPanel.SetActive(false); collectionButton.interactable = true; }
    void CloseCollectionPanel() { collectionPanel.SetActive(false); mapButton.interactable = true; }

}





