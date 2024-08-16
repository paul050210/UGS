using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CameraMove : MonoBehaviour
{
    private Vector3 dir = Vector3.zero;
    private bool isMoving = false;
    public bool lockActivate = false;
    private float moveDelay = 1.0f;
    [SerializeField] private GameObject[] canvases;

    private int temp;

    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    private EventTrigger leftTrigger;
    private EventTrigger rightTrigger;

    private TabletUI tabletUI;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
        tabletUI = FindObjectOfType<TabletUI>();
        leftTrigger = leftButton.GetComponent<EventTrigger>();
        rightTrigger = rightButton.GetComponent<EventTrigger>();
        leftButton.onClick.AddListener(() => Turn(0));
        rightButton.onClick.AddListener(() => Turn(1));

        EventTrigger.Entry entryPointerEnter = new EventTrigger.Entry();
        entryPointerEnter.eventID = EventTriggerType.PointerEnter;
        entryPointerEnter.callback.AddListener((data) => { OnPointerEnter((PointerEventData)data); });

        EventTrigger.Entry entryPointerExit = new EventTrigger.Entry();
        entryPointerExit.eventID = EventTriggerType.PointerExit;
        entryPointerExit.callback.AddListener((data) => { OnPointerExit((PointerEventData)data); });

        leftTrigger.triggers.Add(entryPointerEnter);
        leftTrigger.triggers.Add(entryPointerExit);
        rightTrigger.triggers.Add(entryPointerEnter);
        rightTrigger.triggers.Add(entryPointerExit);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Turn(0);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Turn(1);
        }
    }

    public void Turn(int lr = 0)
    {
        if (tabletUI.IsTabletOn() || !Mathf.Approximately(cam.fieldOfView, 60f)) return;
        if (isMoving) return;
        isMoving = true;
        temp = Mathf.FloorToInt(dir.y / 90f);
        if (lr == 0)
        {
            dir.y -= 90f;
            if (dir.y < 0) dir.y = 270f;
        }
        else
        {
            dir.y += 90f;
            if (dir.y == 360) dir.y = 0f;
        }
        var newCanvas = canvases[Mathf.FloorToInt(dir.y / 90f)];
        newCanvas.SetActive(true);
        newCanvas.transform.GetChild(newCanvas.transform.childCount - 1).gameObject.SetActive(true);
        canvases[temp].transform.GetChild(canvases[temp].transform.childCount - 1).gameObject.SetActive(true);
        transform.DORotate(dir, moveDelay).SetEase(Ease.Linear).OnComplete(() =>
        {
            isMoving = false;
            canvases[temp].SetActive(false);
            newCanvas.transform.GetChild(newCanvas.transform.childCount - 1).gameObject.SetActive(false);
        });
    }
    public void LockCamera(bool lockActivate)
    {
        // 태블릿 왼쪽 및 오른쪽 버튼을 비활성화
        SetButtonInteractable(leftButton, lockActivate);
        SetButtonInteractable(rightButton, lockActivate);
    }

    private void SetButtonInteractable(Button button, bool lockActivate)
    {
        if (button != null)
        {
            button.interactable = !lockActivate;
        }
    }
    private void OnPointerEnter(PointerEventData data)
    {
        if (tabletUI.IsTabletOn() || !Mathf.Approximately(cam.fieldOfView, 60f)) return;
        var img = data.pointerEnter.GetComponent<Image>();
        img.color = new Color(img.color.r, img.color.g, img.color.b, 150f / 255f);
    }

    public void OnPointerExit(PointerEventData data)
    {
        var img = data.pointerEnter.GetComponent<Image>();
        img.color = new Color(img.color.r, img.color.g, img.color.b, 0f);
    }
}
