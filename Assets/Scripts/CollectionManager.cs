using UnityEngine;
using UnityEngine.UI;
public class CollectionManager : MonoBehaviour
{
    public GameObject[] pages; public Button nextPageButton; public Button previousPageButton; private int currentPageIndex = 0;
    void Start()
    {
        ShowPage(currentPageIndex);
        nextPageButton.onClick.AddListener(NextPage); previousPageButton.onClick.AddListener(PreviousPage);
    }
    public void ShowPage(int pageIndex)
    {
        for (int i = 0; i < pages.Length; i++) { pages[i].SetActive(i == pageIndex); }
        previousPageButton.interactable = (pageIndex > 0); nextPageButton.interactable = (pageIndex < pages.Length - 1);
    }
    void NextPage() { if (currentPageIndex < pages.Length - 1) { currentPageIndex++; ShowPage(currentPageIndex); } }
    void PreviousPage() { if (currentPageIndex > 0) { currentPageIndex--; ShowPage(currentPageIndex); } }
}






