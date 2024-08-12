using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WestUI : MonoBehaviour
{
    [SerializeField] private Button mapButton;
    [SerializeField] private Button mapCloseButton;
    [SerializeField] private Button dictionaryButton;
    [SerializeField] private Button dictionaryCloseButton;
    [SerializeField] private Image leftBtn;
    [SerializeField] private Image rightBtn;
    [SerializeField] private GameObject mapPanel;
    [SerializeField] private GameObject dictionaryPanel;

    private Camera cam;
    private float fov = 30f;
    private float camCloseDuration = 0.3f;
    private Vector3 defaultCamPos = new Vector3(0, 1f, 0f);
    private Vector3 mapPanelCamPos = new Vector3(0f, 0.9f, 0f);
    private Vector3 dictionaryPanelCamPos = new Vector3(0f, -1.0f, 0f);

    private void Start()
    {
        mapPanel.SetActive(false);
        dictionaryPanel.SetActive(false);
        mapButton.onClick.AddListener(ShowMapPanel);
        dictionaryButton.onClick.AddListener(ShowDictionaryPanel);
        mapCloseButton.onClick.AddListener(CloseMapBtnClicked);
        dictionaryCloseButton.onClick.AddListener(CloseDictionaryPanel);
        cam = Camera.main;
        cam.gameObject.transform.position = defaultCamPos;
    }

    private void ShowMapPanel()
    {
        if (Mathf.Approximately(cam.fieldOfView, 60f))
        {
            fov = 46f;
            cam.DOFieldOfView(fov, camCloseDuration).SetEase(Ease.Linear).OnComplete(() =>
            {
                mapPanel.SetActive(true);
                mapCloseButton.gameObject.SetActive(true);
                dictionaryPanel.SetActive(false);
                dictionaryButton.gameObject.SetActive(false);


                leftBtn.raycastTarget = false;
                rightBtn.raycastTarget = false;
            });
            cam.gameObject.transform.DOMove(mapPanelCamPos, camCloseDuration).SetEase(Ease.Linear);
        }
    }

    private void ShowDictionaryPanel()
    {
        if (Mathf.Approximately(cam.fieldOfView, 60f))
        {
            fov = 30f;
            cam.DOFieldOfView(fov, camCloseDuration).SetEase(Ease.Linear).OnComplete(() =>
            {
                mapPanel.SetActive(false);
                mapCloseButton.gameObject.SetActive(false);
                dictionaryPanel.SetActive(true);
                dictionaryCloseButton.gameObject.SetActive(true);


            });
            cam.gameObject.transform.DOMove(dictionaryPanelCamPos, camCloseDuration).SetEase(Ease.Linear);
        }
    }

    private void CloseMapBtnClicked()
    {
        // 거래 버튼 확대 상태에서 클릭 시
        if (Mathf.Approximately(cam.fieldOfView, 30f))
        {
            TradeNpcControl.Instance.TurnOffAll();
            fov = 46f; // Ensure it returns to 46f, not 60f
            cam.DOFieldOfView(fov, camCloseDuration).SetEase(Ease.Linear).OnComplete(() =>
            {
                mapPanel.SetActive(true);
                mapCloseButton.gameObject.SetActive(true);
                dictionaryPanel.SetActive(false);
                dictionaryCloseButton.gameObject.SetActive(false);
            });
            cam.gameObject.transform.DOMove(defaultCamPos, camCloseDuration).SetEase(Ease.Linear);
        }
        // 풀 맵 상태에서 클릭 시
        else if (Mathf.Approximately(cam.fieldOfView, 46f))
        {
            mapPanel.SetActive(false);
            mapCloseButton.gameObject.SetActive(false);
            dictionaryPanel.SetActive(false);
            dictionaryCloseButton.gameObject.SetActive(false);
            dictionaryButton.gameObject.SetActive(true);
            leftBtn.raycastTarget = true;
            rightBtn.raycastTarget = true;
            fov = 60f;
            cam.DOFieldOfView(fov, camCloseDuration).SetEase(Ease.Linear);        
            cam.gameObject.transform.DOMove(defaultCamPos, camCloseDuration).SetEase(Ease.Linear);
        }
    }

    private void CloseDictionaryPanel()
    {
        mapPanel.SetActive(false);
        mapCloseButton.gameObject.SetActive(false);
        dictionaryPanel.SetActive(false);
        dictionaryCloseButton.gameObject.SetActive(false); 
        fov = 60f;
        cam.DOFieldOfView(fov, camCloseDuration).SetEase(Ease.Linear);
        cam.gameObject.transform.DOMove(defaultCamPos, camCloseDuration).SetEase(Ease.Linear);
    }
}
