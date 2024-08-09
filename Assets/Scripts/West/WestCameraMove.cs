using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WestCameraMove : MonoBehaviour
{
    public GameObject mapPanel;
    public GameObject dictionaryPanel;
    public Button mapButton;
    public Button dictionaryButton;
    public Button mapCloseButton;
    public Button dictionaryCloseButton;
    

    private Camera cam;
    private float fov = 30f;
    private float camCloseDuration = 0.3f;
    private Vector3 defaultCamPos = new Vector3(0, 1f, 0f);
    private Vector3 mapPanelCamPos = new Vector3(0f, 1.2f, 0f);
    private Vector3 dictionaryPanelCamPos = new Vector3(0f, -1.4f, 0f);

    void Start()
    {
        cam = Camera.main;
        mapPanel.SetActive(false);
        dictionaryPanel.SetActive(false);
        mapButton.onClick.AddListener(ShowMapPanel);
        dictionaryButton.onClick.AddListener(ShowDictionaryPanel);
        mapCloseButton.onClick.AddListener(CloseMapPanel);
        dictionaryCloseButton.onClick.AddListener(CloseDictionaryPanel);

        // Ensure initial camera position
        cam.gameObject.transform.position = defaultCamPos;
    }

    void ShowMapPanel()
    {
        if (Mathf.Approximately(cam.fieldOfView, 60f))
        {
            fov = 45f;
            cam.DOFieldOfView(fov, camCloseDuration).SetEase(Ease.Linear).OnComplete(() =>
            {
                mapPanel.SetActive(true);
                dictionaryPanel.SetActive(false);
                dictionaryButton.interactable = false; // Disable the dictionary button
            });
            cam.gameObject.transform.DOMove(mapPanelCamPos, camCloseDuration).SetEase(Ease.Linear);
        }
    }

    void ShowDictionaryPanel()
    {
        if (Mathf.Approximately(cam.fieldOfView, 60f))
        {
            fov = 29f;
            cam.DOFieldOfView(fov, camCloseDuration).SetEase(Ease.Linear).OnComplete(() =>
            {
                mapPanel.SetActive(false);
                dictionaryPanel.SetActive(true);
                mapButton.interactable = false; // Disable the map button
                
            });
            cam.gameObject.transform.DOMove(dictionaryPanelCamPos, camCloseDuration).SetEase(Ease.Linear);
        }
    }

    void CloseMapPanel()
    {
        mapPanel.SetActive(false);
        dictionaryButton.interactable = true;
        ResetCamera();
    }

    void CloseDictionaryPanel()
    {
        dictionaryPanel.SetActive(false);
        mapButton.interactable = true;
        ResetCamera();
    }

    void ResetCamera()
    {
        fov = 60f;
        cam.DOFieldOfView(fov, camCloseDuration).SetEase(Ease.Linear);
        cam.gameObject.transform.DOMove(defaultCamPos, camCloseDuration).SetEase(Ease.Linear);
    }
}
