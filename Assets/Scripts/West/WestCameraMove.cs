using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WestCameraMove : MonoBehaviour
{
    public GameObject mapPanel;
    public GameObject collectionPanel;
    public Button mapButton;
    public Button collectionButton;
    public Button mapCloseButton;
    public Button collectionCloseButton;
    

    private Camera cam;
    private float fov = 30f;
    private float camCloseDuration = 0.3f;
    private Vector3 defaultCamPos = new Vector3(0, 1f, 0f);
    private Vector3 mapPanelCamPos = new Vector3(0f, 1.2f, 0f);
    private Vector3 collectionPanelCamPos = new Vector3(0f, -1.4f, 0f);

    void Start()
    {
        cam = Camera.main;
        mapPanel.SetActive(false);
        collectionPanel.SetActive(false);
        mapButton.onClick.AddListener(ShowMapPanel);
        collectionButton.onClick.AddListener(ShowCollectionPanel);
        mapCloseButton.onClick.AddListener(CloseMapPanel);
        collectionCloseButton.onClick.AddListener(CloseCollectionPanel);

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
                collectionPanel.SetActive(false);
                collectionButton.interactable = false; // Disable the collection button
            });
            cam.gameObject.transform.DOMove(mapPanelCamPos, camCloseDuration).SetEase(Ease.Linear);
        }
    }

    void ShowCollectionPanel()
    {
        if (Mathf.Approximately(cam.fieldOfView, 60f))
        {
            fov = 29f;
            cam.DOFieldOfView(fov, camCloseDuration).SetEase(Ease.Linear).OnComplete(() =>
            {
                mapPanel.SetActive(false);
                collectionPanel.SetActive(true);
                mapButton.interactable = false; // Disable the map button
                
            });
            cam.gameObject.transform.DOMove(collectionPanelCamPos, camCloseDuration).SetEase(Ease.Linear);
        }
    }

    void CloseMapPanel()
    {
        mapPanel.SetActive(false);
        collectionButton.interactable = true;
        ResetCamera();
    }

    void CloseCollectionPanel()
    {
        collectionPanel.SetActive(false);
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
