using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

public class NorthUI : MonoBehaviour
{
    [SerializeField] private Button zoomBtn;
    [SerializeField] private Button zoomOutBtn;
    [SerializeField] private Button openBtn;
    [SerializeField] private Button closeBtn;
    [SerializeField] private Image leftBtn;
    [SerializeField] private Image rightBtn;
    [SerializeField] private Text dayText;
    [SerializeField] private GameObject questWindow;

    private Camera cam;
    private float fov = 60f;
    private float camY = 1f;
    private float camCloseDuration = 0.3f;

    [HideInInspector]
    public UnityEvent OnDisable;

    private void Awake()
    {
        cam = Camera.main;
        fov = 40f;
        camY = 2.5f;
        cam.DOFieldOfView(fov, camCloseDuration).SetEase(Ease.Linear);
        cam.gameObject.transform.DOMoveY(camY, camCloseDuration).SetEase(Ease.Linear).OnComplete(() =>
        {
            zoomOutBtn.gameObject.SetActive(true);
            openBtn.gameObject.SetActive(true);
            leftBtn.raycastTarget = false;
            rightBtn.raycastTarget = false;
        });
    }

    private void Start()
    {
        cam = Camera.main;
        zoomBtn.onClick.AddListener(Zoom);
        zoomOutBtn.onClick.AddListener(ZoomOut);
        openBtn.onClick.AddListener(OpenWindow);
        closeBtn.onClick.AddListener(CloseWindow);
        GameManager.Instance.OnDayChanged.AddListener(() => 
        {
            dayText.text = $"Day{GameManager.Instance.CurrentDay}";
        });
        dayText.text = $"Day{GameManager.Instance.CurrentDay}";
    }

    private void Zoom()
    {
        if (!Mathf.Approximately(cam.fieldOfView, 60f)) return;
        fov = 40f;
        camY = 2.5f;
        cam.DOFieldOfView(fov, camCloseDuration).SetEase(Ease.Linear);
        cam.gameObject.transform.DOMoveY(camY, camCloseDuration).SetEase(Ease.Linear).OnComplete(() =>
        {
            zoomOutBtn.gameObject.SetActive(true);
            openBtn.gameObject.SetActive(true);
            leftBtn.raycastTarget = false;
            rightBtn.raycastTarget = false;
        });
    }

    private void ZoomOut()
    {
        if (!Mathf.Approximately(cam.fieldOfView, 40f)) return;
        fov = 60f;
        camY = 1f;
        zoomOutBtn.gameObject.SetActive(false);
        openBtn.gameObject.SetActive(false);
        leftBtn.raycastTarget = true;
        rightBtn.raycastTarget = true;
        cam.DOFieldOfView(fov, camCloseDuration).SetEase(Ease.Linear);
        cam.gameObject.transform.DOMoveY(camY, camCloseDuration).SetEase(Ease.Linear);
    }

    private void OpenWindow()
    {
        //TODO: 페이드인 효과
        questWindow.SetActive(true);
    }

    public void CloseWindow()
    {
        questWindow.SetActive(false);
        ItemManager.Instance.canSelect = false;
        OnDisable.Invoke();
    }

    public void SetActiveCloseBtn(bool on)
    {
        closeBtn.gameObject.SetActive(on);
    }



}
