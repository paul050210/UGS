using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NorthUI : MonoBehaviour
{
    [SerializeField] private Button zoomBtn;
    [SerializeField] private Button zoomOutBtn;
    [SerializeField] private Image leftBtn;
    [SerializeField] private Image rightBtn;
    [SerializeField] private GameObject questWindow;

    private Camera cam;
    private float fov = 60f;
    private float camY = 1f;
    private float camCloseDuration = 0.3f;

    private void Start()
    {
        cam = Camera.main;
        zoomBtn.onClick.AddListener(Zoom);
        zoomOutBtn.onClick.AddListener(ZoomOut);
    }

    private void Zoom()
    {
        if (!Mathf.Approximately(cam.fieldOfView, 60f)) return;
        fov = 40f;
        camY = 2.5f;
        cam.DOFieldOfView(fov, camCloseDuration).SetEase(Ease.Linear);
        cam.gameObject.transform.DOMoveY(camY, camCloseDuration).SetEase(Ease.Linear).OnComplete(() =>
        {
            questWindow.SetActive(true);
            leftBtn.raycastTarget = false;
            rightBtn.raycastTarget = false;
        });
    }

    private void ZoomOut()
    {
        if (fov == 60f) return;
        fov = 60f;
        camY = 1f;
        questWindow.SetActive(false);
        leftBtn.raycastTarget = true;
        rightBtn.raycastTarget = true;
        cam.DOFieldOfView(fov, camCloseDuration).SetEase(Ease.Linear);
        cam.gameObject.transform.DOMoveY(camY, camCloseDuration).SetEase(Ease.Linear);
    }
}
