using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CameraMove : MonoBehaviour
{
    private Vector3 dir = Vector3.zero;
    private bool isMoving = false;
    private float moveDelay = 1.0f;
    [SerializeField] private GameObject[] canvases;
    private int temp;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;

    private void Start()
    {
        leftButton.onClick.AddListener(() => Turn(0));
        rightButton.onClick.AddListener(() => Turn(1));
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Turn(0);
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            Turn(1);
        }
    }

    private void Turn(int lr = 0)
    {
        if (isMoving) return;
        isMoving = true;
        temp = Mathf.FloorToInt(dir.y / 90f);
        if(lr == 0)
        {
            dir.y -= 90f;
            if (dir.y < 0) dir.y = 270f;
        }
        else
        {
            dir.y += 90f;
            if (dir.y == 360) dir.y = 0f;
        }
        canvases[Mathf.FloorToInt(dir.y / 90f)].SetActive(true);
        transform.DORotate(dir, moveDelay).SetEase(Ease.Linear).OnComplete(() =>
        {
            isMoving = false;
            canvases[temp].SetActive(false);
        });
    }

}
